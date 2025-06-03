using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace DataLayer
{
    public class DataRepository : IDataRepository
    {
        private readonly ShopDataBaseDataContext _context;

        private DataRepository(string connectionString)
        {
            _context = new ShopDataBaseDataContext(connectionString);
        }

        public static DataRepository CreateNewRepository(string connectionString)
        {
            return new DataRepository(connectionString);
        }

        public double GetCurrentCash()
        {
            var state = _context.StoreStates.FirstOrDefault();
            return state?.TotalFunds ?? 0.0;
        }

        public void UpdateCash(double amount)
        {
            var state = _context.StoreStates.FirstOrDefault();
            if (state == null)
            {
                state = new StoreState { TotalFunds = amount };
                _context.StoreStates.InsertOnSubmit(state);
            }
            else
            {
                state.TotalFunds = amount;
            }
            _context.SubmitChanges();
        }

        public Dictionary<string, (int quantity, double price)> GetAllInventory()
        {
            return _context.Inventories.ToDictionary(
                i => i.ProductName,
                i => (i.Quantity, i.Price)
            );
        }

        public void UpdateInventory(string product, int quantity, double price)
        {
            var inventory = _context.Inventories.FirstOrDefault(i => i.ProductName == product);
            if (inventory == null)
            {
                inventory = new Inventory
                {
                    ProductName = product,
                    Quantity = quantity,
                    Price = price
                };
                _context.Inventories.InsertOnSubmit(inventory);
            }
            else
            {
                inventory.Quantity = quantity;
                inventory.Price = price;
            }
            _context.SubmitChanges();
        }

        public void RemoveFromInventory(string product)
        {
            var inventory = _context.Inventories.FirstOrDefault(i => i.ProductName == product);
            if (inventory != null)
            {
                _context.Inventories.DeleteOnSubmit(inventory);
                _context.SubmitChanges();
            }
        }

        public void SaveEventLog(EventLog log)
        {
            var eventLog = new EventLogEntry
            {
                Timestamp = log.Timestamp,
                EventType = log.EventType,
                Message = log.Message,
                Details = System.Text.Json.JsonSerializer.Serialize(log.Details)
            };
            _context.EventLogs.InsertOnSubmit(eventLog);
            _context.SubmitChanges();
        }

        public void ClearEventLogs()
        {
            _context.EventLogs.DeleteAllOnSubmit(_context.EventLogs);
            _context.SubmitChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void AddUser(int id, string type)
        {
            var user = new User { Id = id, Type = type };
            _context.Users.InsertOnSubmit(user);
            _context.SubmitChanges();
        }
        //LINQ method syntax musi byc
        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public void RemoveUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.DeleteOnSubmit(user);
                _context.SubmitChanges();
            }
        }

        public void AddCatalog(string product, float price)
        {
            var catalog = new Catalog { Product = product, Price = price };
            _context.Catalogs.InsertOnSubmit(catalog);
            _context.SubmitChanges();
        }

        public Catalog GetCatalog(string product)
        {
            return _context.Catalogs.FirstOrDefault(c => c.Product == product);
        }

        public IEnumerable<Catalog> GetAllCatalogs()
        {
            return _context.Catalogs;
        }

        public void RemoveCatalog(string product)
        {
            var catalog = _context.Catalogs.FirstOrDefault(c => c.Product == product);
            if (catalog != null)
            {
                _context.Catalogs.DeleteOnSubmit(catalog);
                _context.SubmitChanges();
            }
        }

        public void AddState(string product, int amount)
        {
            var state = new State { Product = product, Amount = amount };
            _context.States.InsertOnSubmit(state);
            _context.SubmitChanges();
        }

        public State GetState(string product)
        {
            return _context.States.FirstOrDefault(s => s.Product == product);
        }

        public IEnumerable<State> GetAllStates()
        {
            return _context.States;
        }

        public void RemoveState(string product)
        {
            var state = _context.States.FirstOrDefault(s => s.Product == product);
            if (state != null)
            {
                _context.States.DeleteOnSubmit(state);
                _context.SubmitChanges();
            }
        }

        public void UpdateState(string product, int newAmount)
        {
            var state = _context.States.FirstOrDefault(s => s.Product == product);
            if (state != null)
            {
                state.Amount = newAmount;
                _context.SubmitChanges();
            }
        }

        public void ClearAll()
        {
            _context.Users.DeleteAllOnSubmit(_context.Users);
            _context.Catalogs.DeleteAllOnSubmit(_context.Catalogs);
            _context.States.DeleteAllOnSubmit(_context.States);
            _context.SubmitChanges();
        }
    }
} 