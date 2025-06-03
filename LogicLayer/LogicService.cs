using System.Collections.Generic;
using System.Linq;
using DataLayer;

namespace LogicLayer
{
    public class LogicService : ILogicService
    {
        private readonly IEvents _events;
        private readonly Supply_class _supplyService;
        private readonly Buy_class _buyService;
        private readonly Login_class _loginService;

        public LogicService(IState state, IEvents events)
        {
            _events = events;

            // Supply_class and Buy_class depend on IState and IEvents.
            // These dependencies are now provided to LogicService's constructor.
            _supplyService = new Supply_class(state, events);
            _buyService = new Buy_class(state, events);
            _loginService = new Login_class(events);
        }

        public void AddProduct(string name, float price)
        {
            _events.Add2Cat(name, price);
        }

        public Dictionary<string, float> GetAllProducts()
        {
            var products = from kvp in _events.GetAllProducts()
                         where kvp.Key != null
                         select new { Key = kvp.Key!, Value = kvp.Value };
            return products.ToDictionary(p => p.Key, p => p.Value);
        }

        public void AddSupply(string productName, int quantity)
        {
            _supplyService.Supply(productName, quantity);
        }

        /*public int GetProductQuantity(string productName)
        {
            var inventory = _events.GetAllInventory();
            return inventory.TryGetValue(productName, out int amount) ? amount : 0;
        }*/

        public bool BuyProduct(string productName, int quantity)
        {
            return _buyService.Buy(productName, quantity);
        }

        public void AddUser(int userId, string userType)
        {
            _events.Add2Users(userId, userType);
        }

        public Dictionary<int, string> GetAllUsers()
        {
            var users = from kvp in _events.GetAllUsers()
                       where kvp.Key != null
                       select new { Key = kvp.Key!.Value, Value = kvp.Value };
            return users.ToDictionary(u => u.Key, u => u.Value);
        }

        public void AddToState(string productName, int amount)
        {
            _events.Add2State(productName, amount);
        }

        public Dictionary<string, int> GetAllInventory()
        {
            var inventory = from kvp in _events.GetAllInventory()
                           where kvp.Key != null
                           select new { Key = kvp.Key!, Value = kvp.Value };
            return inventory.ToDictionary(i => i.Key, i => i.Value);
        }

        public float GetCurrentCash()
        {
            return _events.GetCurrentCash();
        }
    }
} 