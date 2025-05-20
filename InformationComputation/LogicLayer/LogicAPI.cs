using System;
using System.Collections.Generic;
using DataLayer;
using UserType = DataLayer.User_data.UserType;

namespace LogicLayer
{
    public abstract class LogicAPI
    {
        public abstract bool Purchase(string name, int amount);
        public abstract double GetPrice(string name);
        public abstract bool Shipment(string productName, int amount);
        public abstract Dictionary<string, (int quantity, double price)> GetInventory();
        public abstract double GetFunds();
        public abstract bool ItemRanOut(string prod_name);
        public abstract bool CanPurchase(long id);
        public abstract bool CanSupply(long id);
        public abstract void AddUser(long id, UserType userType);
    }

    public class Logic : LogicAPI
    {
        private readonly Events _events;
        private readonly Purchase_logic _purchaseLogic;
        private readonly Shipment_logic _shipmentLogic;
        private readonly OutOfStock _outOfStockLogic;
        private readonly RegisterUser _registerUserLogic;

        public Logic(Events events)
        {
            _events = events;
            _purchaseLogic = new PurchaseLogic(events);
            _shipmentLogic = new ShipmentLogic(events);
            _outOfStockLogic = new OutOfStockLogic(events);
            _registerUserLogic = new RegisterUserLogic(events);
        }

        public override bool Purchase(string name, int amount) => _purchaseLogic.Purchase(name, amount);
        public override double GetPrice(string name) => _purchaseLogic.GetPrice(name);
        public override bool Shipment(string productName, int amount) => _shipmentLogic.Shipment(productName, amount);
        public override Dictionary<string, (int quantity, double price)> GetInventory() => _shipmentLogic.GetInventory();
        public override double GetFunds() => _shipmentLogic.GetFunds();
        public override bool ItemRanOut(string prod_name) => _outOfStockLogic.ItemRanOut(prod_name);
        public override bool CanPurchase(long id) => _registerUserLogic.CanPurchase(id);
        public override bool CanSupply(long id) => _registerUserLogic.CanSupply(id);
        public override void AddUser(long id, UserType userType) => _registerUserLogic.AddUser(id, userType);
    }

    public static class LogicFactory
    {
        public static LogicAPI Create()
        {
            var events = new DataLayer.Events();
            return new Logic(events);
        }
    }
} 