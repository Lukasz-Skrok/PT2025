using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    internal class Catalog_class
    {
        private Dictionary<string, float> prices = new();

        public float GetPrice(string product)
        {
            return prices[product];
        }
        public void Add2Cat(string name, float price)
        {
            prices.Add(name, price);
        }

        public Dictionary<string, float> GetAllProducts()
        {
            return prices;
        }
    }
}
