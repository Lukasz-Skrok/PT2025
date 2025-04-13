using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static DataLayer.User_data;

namespace LogicLayerTests
{

    internal class TestEvents : Events
    {
        private User_data users = new();
        private Item_catalog catalog = new();
        private Store_state store = new();
        float totalFunds = (float)15.0;
        public override int GetAmount(string prod_name)
        {
            return store.GetAmount(prod_name);
        }
        public override UserType GetUser(long id)
        {
            return users.GetUser(id);
        }
        public override float GetPrice(string name)
        {
            return catalog.GetPrice(name);
        }
        public override bool CheckStorage(string prod_name, int amount)
        {
            if (store.GetAmount(prod_name) >= amount)
                return true;
            return false;
        }
        public override bool CheckFunds(float price)
        {
            if (price <= totalFunds)
                return true;
            return false;
    }
        public override void IncreaseStock(string name, int amount)
        {
            store.IncreaseStock(name, amount);
        }
        public override void RecordProfit(float money)
        {
            store.RecordProfit(money);
        }
        public override void AddItem(string prod_name, float price)
        {
            this.catalog.AddItem(prod_name, price);
        }
        public override void RemoveItem(string prod_name)
        {
            catalog.RemoveItem(prod_name);
        }
        public override void AddToStorage(string prod_name, int amount)
        {
            this.store.AddToStorage(prod_name, amount);
        }
        public override void RemoveFromStorage(string prod_name)
        {
            store.RemoveFromStorage(prod_name);
        }
        public override void AddUser(long id, UserType type)
        {
            users.AddUser(id, type);
        }
        public override void RemoveUser(long id)
        {
            users.RemoveUser(id);
        }
    }
}
