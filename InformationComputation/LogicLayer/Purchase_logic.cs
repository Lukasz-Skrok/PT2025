using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace LogicLayer
{
    public abstract class Purchase_logic
    {
        protected readonly Events events1;
        public Purchase_logic(Events events)
        {
            events1 = events;
        }
        public abstract bool Purchase(string name, int amount);
        public abstract double GetPrice(string name);
    }

    public class PurchaseLogic : Purchase_logic
    {
        public PurchaseLogic(Events events) : base(events) { }

        public override bool Purchase(string name, int amount)
        {
            try
            {
                bool inStorage = events1.CheckStorage(name, amount);
                if (!inStorage) return false;

                double cost = amount * events1.GetPrice(name);
                events1.AddToStorage(name, -amount);
                events1.RecordProfit(cost);
                return true;
            }
            catch
            {
                // Item not found or other issue
                return false;
            }
        }

        public override double GetPrice(string name)
        {
            try
            {
                return events1.GetPrice(name);
            }
            catch
            {
                return -1; // Or handle it differently if needed
            }
        }
    }
}
