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
        private float totalFunds=0;
        private Dictionary<string, int> itemStorage = new Dictionary<string, int>();
        
        public void AddToStorage(string prod_name, int amount)
        {
            itemStorage.Add(prod_name, amount);
        }
        public void RemoveFromStorage(string prod_name)
        {
            itemStorage.Remove(prod_name);
        }
        public bool CheckStorage(string prod_name, int amount)
        {
            int stock = itemStorage[prod_name];
            if (stock >= amount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetAmount(string prod_name)
        {
            return itemStorage[prod_name];
        }
        public bool CheckFunds(float price)
        {
            if (price < totalFunds)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void IncreaseStock(string prod_name, int amount)
        {
            itemStorage[prod_name] += amount;
        }
        public void RecordProfit(float money)
        {
            totalFunds += money;
        }
    }
}
