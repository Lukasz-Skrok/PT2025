using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationTest
{
    internal class FakeService : ILogicService
    {
        private Dictionary<int, string> users = new();
        private Dictionary<string?, int> state = new();
        private Dictionary<string?, float> catalog = new();
        float cash = (float)20.0;
        public void AddProduct(string name, float price)
        {
            catalog.Add(name, price);
        }

        public void AddSupply(string productName, int quantity)
        {
            state[productName] += quantity;
        }

        public void AddToState(string productName, int amount)
        {
            state.Add(productName, amount);
        }

        public void AddUser(int userId, string userType)
        {
            users.Add(userId, userType);
        }

        public bool BuyProduct(string productName, int quantity)
        {
            if (state[productName]-quantity > 0)
            {
                state[productName] -= quantity;
                cash += catalog[productName] * quantity;
                return true;
            }
            return false;
        }

        public Dictionary<string, int> GetAllInventory()
        {
            //throw new NotImplementedException();
            return state;
        }

        public Dictionary<string, float> GetAllProducts()
        {
            //throw new NotImplementedException();
            return catalog;
        }

        public Dictionary<int, string> GetAllUsers()
        {
            //throw new NotImplementedException();
            return users;
        }

        public float GetCurrentCash()
        {
            return cash;
        }
    }
}
