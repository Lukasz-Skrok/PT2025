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
    public abstract class DataAPI
    {
        public abstract UserType GetUser(long id);
        public abstract float GetPrice(string name);
        public abstract bool CheckStorage(string prod_name, int amount);
        public abstract bool CheckFunds(float price);
        public abstract void IncreaseStock(string prod_name, int amount);
        public abstract void RecordProfit(float money);
        public abstract void AddItem(string name, float price);
        public abstract void RemoveItem(string name);
        public abstract void AddToStorage(string prod_name, int amount);
        public abstract void RemoveFromStorage(string prod_name);
        public abstract void AddUser(long id, UserType type);
        public abstract void RemoveUser(long id);
        public abstract int GetAmount(string prod_name);
    }

    public class Events : DataAPI
    {
        private User_data users = new UserData();
        private Item_catalog catalog = new ItemCatalog();
        private Store_state store = new StoreState();

        public override int GetAmount(string prod_name) => store.GetAmount(prod_name);
        public override UserType GetUser(long id) => users.GetUser(id);
        public override float GetPrice(string name) => catalog.GetPrice(name);
        public override bool CheckStorage(string prod_name, int amount) => store.CheckStorage(prod_name, amount);
        public override bool CheckFunds(float price) => store.CheckFunds(price);
        public override void IncreaseStock(string name, int amount) => store.IncreaseStock(name, amount);
        public override void RecordProfit(float money) => store.RecordProfit(money);
        public override void AddItem(string name, float price) => catalog.AddItem(name, price);
        public override void RemoveItem(string name) => catalog.RemoveItem(name);
        public override void AddToStorage(string name, int amount) => store.AddToStorage(name, amount);
        public override void RemoveFromStorage(string name) => store.RemoveFromStorage(name);
        public override void AddUser(long id, UserType type) => users.AddUser(id, type);
        public override void RemoveUser(long id) => users.RemoveUser(id);
    }
}