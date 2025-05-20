using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;

namespace DataLayer
{
    [Table(Name = "StoreState")]
    public class Store_state
    {
        static Store_state()
        {
            DatabaseInitializer.Initialize();
        }

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public double TotalFunds { get; set; }

        public double GetFunds()
        {
            using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
            {
                return db.StoreStates.First().TotalFunds;
            }
        }

        public void RecordProfit(double profit)
        {
            using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
            {
                var storeState = db.StoreStates.First();
                storeState.TotalFunds += profit;
                db.SubmitChanges();
            }
        }

        public bool CheckFunds(double amount)
        {
            return GetFunds() >= amount;
        }

        public bool CheckStorage(string name, int amount)
        {
            using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
            {
                var inventory = db.Inventories.FirstOrDefault(i => i.ProductName == name);
                return inventory != null && inventory.Quantity >= amount;
            }
        }

        public double GetPrice(string name)
        {
            using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
            {
                var inventory = db.Inventories.FirstOrDefault(i => i.ProductName == name);
                if (inventory == null)
                    throw new Exception($"Product '{name}' not found.");
                return inventory.Price;
            }
        }

        public void AddToStorage(string name, int amount)
        {
            using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
            {
                var inventory = db.Inventories.FirstOrDefault(i => i.ProductName == name);
                if (inventory != null)
                {
                    inventory.Quantity += amount;
                }
                else
                {
                    inventory = new Inventory
                    {
                        ProductName = name,
                        Quantity = amount,
                        Price = 1.0 // Default price
                    };
                    db.Inventories.InsertOnSubmit(inventory);
                }
                db.SubmitChanges();
            }
        }

        public void RemoveFromStorage(string name)
        {
            using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
            {
                var inventory = db.Inventories.FirstOrDefault(i => i.ProductName == name);
                if (inventory != null)
                {
                    db.Inventories.DeleteOnSubmit(inventory);
                    db.SubmitChanges();
                }
                else
                {
                    throw new Exception($"Product '{name}' not found in storage.");
                }
            }
        }

        public void IncreaseStock(string name, int amount)
        {
            AddToStorage(name, amount);
        }

        public int GetAmount(string name)
        {
            using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
            {
                var inventory = db.Inventories.FirstOrDefault(i => i.ProductName == name);
                return inventory?.Quantity ?? 0;
            }
        }

        public Dictionary<string, (int quantity, double price)> GetInventory()
        {
            using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
            {
                return db.Inventories.ToDictionary(
                    i => i.ProductName,
                    i => (i.Quantity, i.Price)
                );
            }
        }

        public List<Inventory> GetAllInventory_QuerySyntax()
        {
            using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
            {
                var inventory = (from inv in db.Inventories select inv).ToList();
                return inventory;
            }
        }
    }
}
