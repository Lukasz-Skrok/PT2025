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
    public class UserData : User_data { };
    public class ItemCatalog : Item_catalog { };
    public class StoreState : Store_state { };
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
        private User_data users = new UserData();
        private Item_catalog catalog = new ItemCatalog();
        private Store_state store = new StoreState();
        public virtual int GetAmount(string prod_name)
        {
            return store.GetAmount(prod_name);
        }
        public virtual UserType GetUser(long id)
        {
            return users.GetUser(id);
        }
        public virtual float GetPrice(string name)
        {
            return catalog.GetPrice(name);
        }
        public virtual bool CheckStorage(string prod_name, int amount)
        {
            return store.CheckStorage(prod_name, amount);
        }
        public virtual bool CheckFunds(float price) 
        {
            return store.CheckFunds(price);
        }
        public virtual void IncreaseStock(string name, int amount) 
        {
            store.IncreaseStock(name, amount);
        }
        public virtual void RecordProfit(float money) 
        { 
            store.RecordProfit(money);
        }
        public virtual void AddItem(string prod_name, float price)
        {
            catalog.AddItem(prod_name, price);
        }
        public virtual void RemoveItem(string prod_name)
        {
            catalog.RemoveItem(prod_name);
        }
        public virtual void AddToStorage(string prod_name, int amount)
        {
            store.AddToStorage(prod_name, amount);
        }
        public virtual void RemoveFromStorage(string prod_name)
        {
            store.RemoveFromStorage(prod_name);
        }
        public virtual void AddUser(long id, UserType type)
        {
            users.AddUser(id, type);
        }
        public virtual void RemoveUser(long id)
        {
            users.RemoveUser(id);
        }
    }
}
