using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Item_catalog
    {
        public Dictionary<string, float> ItemStorage = new Dictionary<string, float>();
        public void AddItem(string name, float price)
        {
            if (name != null && price > 0)
            {
                ItemStorage.Add(name, price);
            }
        }
        public void RemoveItem(string name)
        {
            ItemStorage.Remove(name);
        }
        public float GetPrice(string name)
        {
            return ItemStorage[name];
        }
    }
}
