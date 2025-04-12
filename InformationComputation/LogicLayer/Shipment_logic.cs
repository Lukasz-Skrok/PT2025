using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace LogicLayer
{
    public class Shipment_logic
    {
        private readonly Events events;
        public Shipment_logic(Events events)
        {
            this.events = events;
        }
        public bool Shipment(string prod_name, int amount)
        {
            float shipment_price = events.GetPrice(prod_name) * amount * (float)0.2;
            shipment_price = (float)Math.Round(shipment_price, 2);
            bool canAfford = events.CheckFunds(shipment_price);
            if (!canAfford)
            {
                return false;
            }
            events.AddToStorage(prod_name, amount);
            events.RecordProfit(shipment_price * (-1));
            return true;
        }
    }
}
