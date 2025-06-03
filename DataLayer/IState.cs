using System;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IState
    {
        bool CheckStock(string product, int amount);
        void Add2State(string name, int amount, double price = 0.0);
        bool CheckMoney(double price);
        void AddStock(string product, int amount);
        void AddMoney(double profit);
        Dictionary<string, (int quantity, double price)> GetAllInventory();
        double GetCurrentCash();
        double GetPrice(string product);
        void RemoveFromInventory(string product);
    }
} 