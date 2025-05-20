using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public abstract class Item_catalog
    {
        public Dictionary<string, float> ItemStorage = new Dictionary<string, float>();
        public void AddItem(string name, float price)
        {
            if (!string.IsNullOrWhiteSpace(name) && price > 0)
            {
                if (!ItemStorage.ContainsKey(name))
                {
                    ItemStorage.Add(name, price);
                }
                // else: optionally update price, or ignore
            }
        }
        public void RemoveItem(string name)
        {
            ItemStorage.Remove(name);
        }
        public float GetPrice(string name)
        {
            if (!ItemStorage.ContainsKey(name))
                throw new KeyNotFoundException($"Item '{name}' not found in the catalog.");
            return ItemStorage[name];
        }
        public bool ContainsItem(string name)
        {
            return ItemStorage.ContainsKey(name);
        }
    }
}
