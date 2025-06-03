using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicTest
{
    internal class FakeEvents : IEvents
    {
        private Dictionary<int?, string> users = new();
        private Dictionary<string?, int> state = new();
        private Dictionary<string?, float> catalog = new();
        float cash = (float)20.0;

        public string FindUser(int id)
        {
            return users[id];
        }

        public bool CheckStock(string product, int amount)
        {
            if (amount <= state[product])
                return true;
            else return false;
        }

        public bool CheckMoney(float price)
        {
           if (price <= cash)
                return true;
           else return false;
        }

        public float GetPrice(string product)
        {
            return catalog[product];
        }

        public void AddMoney(float profit)
        {
           cash+=profit;
        }

        public void AddStock(string product, int amount)
        {
            state[product] += amount;
        }

        public void Add2Cat(string product, float price)
        {
            catalog.Add(product, price);
        }

        public void Add2Users(int id, string type)
        {
            users.Add(id, type);
        }

        public void Add2State(string product, int amount)
        {
            state.Add(product, amount);
        }

        public Dictionary<int?, string> GetAllUsers()
        {
            return users;
        }

        public Dictionary<string?, float> GetAllProducts()
        {
            return catalog;
        }

        public Dictionary<string?, int> GetAllInventory()
        {
            return state;
        }

        public float GetCurrentCash()
        {
            return cash;
        }

        public void ClearLogs() { /* No-op for fake */ }
        public List<EventLog> GetEventLogs() { return new List<EventLog>(); }
        public void LogError(string message) { /* No-op for fake */ }
        public void RecordPurchase(string product, int amount, double totalCost) { /* No-op for fake */ }
        public void RecordSupply(string product, int amount) { /* No-op for fake */ }
    }
}
