using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;

namespace DataLayer
{
    [Table(Name = "StoreState")]
    public class State_class : IState
    {
        private readonly IDataRepository _repository;
        private Dictionary<string, (int quantity, double price)> _inventory;
        private double _cash;

        public State_class(IDataRepository repository)
        {
            _repository = repository;
            _inventory = new Dictionary<string, (int quantity, double price)>();
            LoadState();
        }

        private void LoadState()
        {
            _cash = _repository.GetCurrentCash();
            _inventory = _repository.GetAllInventory();
        }

        public bool CheckStock(string product, int amount)
        {
            if (!_inventory.ContainsKey(product))
                return false;
            return _inventory[product].quantity >= amount;
        }

        public void Add2State(string name, int amount, double price = 0.0)
        {
            if (_inventory.ContainsKey(name))
                throw new InvalidOperationException($"Product '{name}' already exists in inventory.");
            
            _inventory[name] = (amount, price);
            _repository.UpdateInventory(name, amount, price);
        }

        public bool CheckMoney(double price)
        {
            return _cash >= price;
        }

        public void AddStock(string product, int amount)
        {
            if (!_inventory.ContainsKey(product))
                throw new InvalidOperationException($"Product '{product}' not found in inventory.");

            var current = _inventory[product];
            _inventory[product] = (current.quantity + amount, current.price);
            _repository.UpdateInventory(product, current.quantity + amount, current.price);
        }

        public void AddMoney(double profit)
        {
            _cash += profit;
            _repository.UpdateCash(_cash);
        }

        public Dictionary<string, (int quantity, double price)> GetAllInventory()
        {
            return new Dictionary<string, (int quantity, double price)>(_inventory);
        }

        public double GetCurrentCash()
        {
            return _cash;
        }

        public double GetPrice(string product)
        {
            if (!_inventory.ContainsKey(product))
                throw new InvalidOperationException($"Product '{product}' not found in inventory.");
            return _inventory[product].price;
        }

        public void RemoveFromInventory(string product)
        {
            if (!_inventory.ContainsKey(product))
                throw new InvalidOperationException($"Product '{product}' not found in inventory.");
            
            _inventory.Remove(product);
            _repository.RemoveFromInventory(product);
        }
    }
}
