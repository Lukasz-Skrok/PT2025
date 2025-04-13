using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace LogicLayer
{
    public class Purchase_logic
    {
        private readonly Events events1;
        public Purchase_logic(Events events)
        {
            events1 = events;
        }
        public bool Purchase(string name, int amount)
        {
            bool inStorage = events1.CheckStorage(name, amount);
            if (!inStorage)
            {
                return false;
            }
            float cost = amount * events1.GetPrice(name);
            events1.AddToStorage(name, (-1) * amount);
            events1.RecordProfit(cost);
            return true;
        }
    }
}
