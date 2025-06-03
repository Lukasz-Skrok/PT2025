using System.Collections.Generic;
using DataLayer;

namespace LogicTest
{
    internal class FakeState : IState
    {
        public bool CheckStock(string product, int amount) => true;
        public void Add2State(string name, int amount, double price = 0.0) { }
        public bool CheckMoney(double price) => true;
        public void AddStock(string product, int amount) { }
        public void AddMoney(double profit) { }
        public Dictionary<string, (int quantity, double price)> GetAllInventory() => new();
        public double GetCurrentCash() => 100.0;
        public double GetPrice(string product) => 1.0;
        public void RemoveFromInventory(string product) { }
    }
} 