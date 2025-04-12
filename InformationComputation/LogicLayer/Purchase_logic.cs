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
        private readonly Events events;
        public Purchase_logic(Events events)
        {
            this.events = events;
        }
        public bool Purchase(string name, int amount)
        {
            bool inStorage = events.CheckStorage(name, amount);
            if (!inStorage)
            {
                return false;
            }
            float cost = amount * events.GetPrice(name);
            events.AddToStorage(name, (-1) * amount);
            events.RecordProfit(cost);
            return true;
        }
    }
}
