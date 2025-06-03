using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer
{
    public abstract class BuyBase
    {
        protected readonly IState State;
        protected readonly IEvents Events;

        protected BuyBase(IState state, IEvents events)
        {
            State = state;
            Events = events;
        }

        public abstract bool Buy(string product, int amount);
        public abstract double GetPrice(string product);
    }

    public class Buy_class : BuyBase
    {
        public Buy_class(IState state, IEvents events) : base(state, events)
        {
        }

        public override bool Buy(string product, int amount)
        {
            try
            {
                if (!State.CheckStock(product, amount))
                    return false;

                double price = State.GetPrice(product);
                double totalCost = price * amount;

                if (!State.CheckMoney(totalCost))
                    return false;

                // Perform the purchase
                State.AddStock(product, -amount);
                State.AddMoney(totalCost);
                Events.RecordPurchase(product, amount, totalCost);
                
                return true;
            }
            catch (Exception ex)
            {
                Events.LogError($"Purchase failed: {ex.Message}");
                return false;
            }
        }

        public override double GetPrice(string product)
        {
            try
            {
                return State.GetPrice(product);
            }
            catch (Exception ex)
            {
                Events.LogError($"Failed to get price: {ex.Message}");
                return -1;
            }
        }
    }
}
