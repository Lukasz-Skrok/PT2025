using System.Collections.Generic;

namespace LogicLayer
{
    public interface ILogicService
    {
        // Product operations
        void AddProduct(string name, float price);
        Dictionary<string, float> GetAllProducts();
        
        // Supply operations
        void AddSupply(string productName, int quantity);
        //int GetProductQuantity(string productName);
        
        // Buy operations
        bool BuyProduct(string productName, int quantity);
        
        // User operations
        void AddUser(int userId, string userType);
        Dictionary<int, string> GetAllUsers();

        // State operations
        void AddToState(string productName, int amount);
        Dictionary<string, int> GetAllInventory();
        float GetCurrentCash();
    }
} 