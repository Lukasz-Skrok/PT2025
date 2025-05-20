using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace DataLayer
{
    [Table(Name = "ItemCatalog")]
    public class Item_catalog
    {
        static Item_catalog()
        {
            DatabaseInitializer.Initialize();
        }

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public double Price { get; set; }

        // Property to maintain backward compatibility
        public Dictionary<string, double> ItemStorage
        {
            get
            {
                var storage = new Dictionary<string, double>();
                using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
                {
                    var items = db.ItemCatalogs.ToList();
                    foreach (var item in items)
                    {
                        storage[item.Name] = item.Price;
                    }
                }
                return storage;
            }
        }

        public void AddItem(string name, double price)
        {
            if (!string.IsNullOrWhiteSpace(name) && price > 0)
            {
                using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
                {
                    var item = new Item_catalog { Name = name, Price = price };
                    db.ItemCatalogs.InsertOnSubmit(item);
                    db.SubmitChanges();
                }
            }
        }

        public void RemoveItem(string name)
        {
            using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
            {
                var item = db.ItemCatalogs.FirstOrDefault(i => i.Name == name);
                if (item != null)
                {
                    db.ItemCatalogs.DeleteOnSubmit(item);
                    db.SubmitChanges();
                }
            }
        }

        public double GetPrice(string name)
        {
            using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
            {
                var item = db.ItemCatalogs.FirstOrDefault(i => i.Name == name);
                if (item == null)
                    throw new KeyNotFoundException($"Item '{name}' not found in the catalog.");
                return item.Price;
            }
        }

        public bool ContainsItem(string name)
        {
            using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
            {
                return db.ItemCatalogs.Any(i => i.Name == name);
            }
        }

        public List<Item_catalog> GetAllItems_QuerySyntax()
        {
            using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
            {
                var items = (from item in db.ItemCatalogs select item).ToList();
                return items;
            }
        }
    }
}
