using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer
{
    public abstract class SupplyBase
    {
        protected readonly IState State;
        protected readonly IEvents Events;

        protected SupplyBase(IState state, IEvents events)
        {
            State = state;
            Events = events;
        }

        public abstract bool Supply(string product, int amount);
        public abstract bool CanSupply(string product, int amount);
    }

    public class Supply_class : SupplyBase
    {
        public Supply_class(IState state, IEvents events) : base(state, events)
        {
        }

        public override bool Supply(string product, int amount)
        {
            try
            {
                if (!CanSupply(product, amount))
                    return false;

                // Add to inventory
                if (State.GetAllInventory().ContainsKey(product))
                {
                    State.AddStock(product, amount);
                }
                else
                {
                    State.Add2State(product, amount);
                }

                Events.RecordSupply(product, amount);
                return true;
            }
            catch (Exception ex)
            {
                Events.LogError($"Supply failed: {ex.Message}");
                return false;
            }
        }

        public override bool CanSupply(string product, int amount)
        {
            try
            {
                if (amount <= 0)
                {
                    Events.LogError("Supply amount must be positive");
                    return false;
                }

                // Check if we have enough money to store the product
                double storageCost = amount * 0.1; // Example storage cost calculation
                return State.CheckMoney(storageCost);
            }
            catch (Exception ex)
            {
                Events.LogError($"Supply validation failed: {ex.Message}");
                return false;
            }
        }
    }
}
