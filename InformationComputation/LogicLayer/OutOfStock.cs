using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace LogicLayer
{
    public abstract class OutOfStock
    {
        protected readonly Events events;
        public OutOfStock(Events events)
        {
            this.events = events;
        }
        public abstract bool ItemRanOut(string prod_name);
    }

    public class OutOfStockLogic : OutOfStock
    {
        public OutOfStockLogic(Events events) : base(events) { }

        public override bool ItemRanOut(string prod_name)
        {
            if (events.GetAmount(prod_name) == 0)
            {
                events.RemoveFromStorage(prod_name);
                return true;
            }
            return false;
        }
    }
}
