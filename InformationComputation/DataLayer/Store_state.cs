using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public abstract class Store_state
    {
        private float totalFunds = 1000.0f; // Initial funds
        private Dictionary<string, (int quantity, float price)> storage = new();

        public Store_state()
        {
            // Initial stock setup
            storage["Apples"] = (50, 2.5f);
            storage["Bananas"] = (30, 1.8f);
            storage["Oranges"] = (40, 3.0f);
        }

        public float GetFunds() => totalFunds;

        public void RecordProfit(float profit)
        {
            totalFunds += profit;
        }

        public bool CheckFunds(float amount)
        {
            return totalFunds >= amount;
        }

        public bool CheckStorage(string name, int amount)
        {
            return storage.ContainsKey(name) && storage[name].quantity >= amount;
        }

        public float GetPrice(string name)
        {
            if (storage.ContainsKey(name))
                return storage[name].price;
            throw new Exception($"Product '{name}' not found.");
        }

        public void AddToStorage(string name, int amount)
        {
            if (storage.ContainsKey(name))
            {
                var (qty, price) = storage[name];
                storage[name] = (qty + amount, price);
            }
            else
            {
                // Default price for new items
                storage[name] = (amount, 1.0f);
            }
        }

        // ✅ Completely remove item from store
        public void RemoveFromStorage(string name)
        {
            if (storage.ContainsKey(name))
            {
                storage.Remove(name);
            }
            else
            {
                throw new Exception($"Product '{name}' not found in storage.");
            }
        }

        // ✅ Increases quantity — alias of AddToStorage
        public void IncreaseStock(string name, int amount)
        {
            AddToStorage(name, amount);
        }

        // ✅ Get quantity of specific product
        public int GetAmount(string name)
        {
            if (storage.ContainsKey(name))
                return storage[name].quantity;
            return 0;
        }

        // Optional: expose full inventory
        public Dictionary<string, (int quantity, float price)> GetInventory()
        {
            return new Dictionary<string, (int, float)>(storage);
        }
    }
}
