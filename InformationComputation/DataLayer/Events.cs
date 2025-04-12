using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static DataLayer.User_data;

namespace DataLayer
{
    public interface Events_interface
    {
        UserType GetUser(long id);
        float GetPrice(string name);
        bool CheckStorage(string prod_name, int amount);
        bool CheckFunds(float price);
        void IncreaseStock(string prod_name, int amount);
        void RecordProfit(float money);
        void AddItem(string name, float price);
        void RemoveItem(string name);
        void AddToStorage(string prod_name, int amount);
        void RemoveFromStorage(string prod_name);
        void AddUser(long id, UserType type);
        void RemoveUser(long id);
        int GetAmount(string prod_name);
    }
    public class Events : Events_interface
    {
        private User_data users = new User_data();
        private Item_catalog catalog = new Item_catalog();
        private Store_state store = new Store_state();
        public int GetAmount(string prod_name)
        {
            return store.GetAmount(prod_name);
        }
        public UserType GetUser(long id)
        {
            return users.GetUser(id);
        }
        public float GetPrice(string name)
        {
            return catalog.GetPrice(name);
        }
        public bool CheckStorage(string prod_name, int amount)
        {
            return store.CheckStorage(prod_name, amount);
        }
        public bool CheckFunds(float price) 
        {
            return store.CheckFunds(price);
        }
        public void IncreaseStock(string name, int amount) 
        {
            store.IncreaseStock(name, amount);
        }
        public void RecordProfit(float money) 
        { 
            store.RecordProfit(money);
        }
        public void AddItem(string prod_name, float price)
        {
            catalog.AddItem(prod_name, price);
        }
        public void RemoveItem(string prod_name)
        {
            catalog.RemoveItem(prod_name);
        }
        public void AddToStorage(string prod_name, int amount)
        {
            store.AddToStorage(prod_name, amount);
        }
        public void RemoveFromStorage(string prod_name)
        {
            store.RemoveFromStorage(prod_name);
        }
        public void AddUser(long id, UserType type)
        {
            users.AddUser(id, type);
        }
        public void RemoveUser(long id)
        {
            users.RemoveUser(id);
        }
    }
}
