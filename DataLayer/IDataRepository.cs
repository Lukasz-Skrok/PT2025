using System;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IDataRepository : IDisposable
    {
        void AddUser(int id, string type);
        User GetUser(int id);
        IEnumerable<User> GetAllUsers();
        void RemoveUser(int id);

        void AddCatalog(string product, float price);
        Catalog GetCatalog(string product);
        IEnumerable<Catalog> GetAllCatalogs();
        void RemoveCatalog(string product);

        void AddState(string product, int amount);
        State GetState(string product);
        IEnumerable<State> GetAllStates();
        void RemoveState(string product);
        void UpdateState(string product, int newAmount);

        //database stuff
        void ClearAll();
        static IDataRepository CreateNewRepository(string connectionString) => throw new NotImplementedException();

        // New methods for state management
        double GetCurrentCash();
        void UpdateCash(double amount);
        Dictionary<string, (int quantity, double price)> GetAllInventory();
        void UpdateInventory(string product, int quantity, double price);
        void RemoveFromInventory(string product);

        // New methods for event logging
        void SaveEventLog(EventLog log);
        void ClearEventLogs();
    }
} 