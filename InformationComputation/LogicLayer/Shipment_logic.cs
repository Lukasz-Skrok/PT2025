using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace LogicLayer
{
    public class Shipment_logic
    {
        private readonly Events events1;
        public Shipment_logic(Events events)
        {
                events1 = events;
        }
        public bool Shipment(string prod_name, int amount)
        {
            try
            {
                float shipment_price = events1.GetPrice(prod_name) * amount * (float)0.2;
                shipment_price = (float)Math.Round(shipment_price, 2);
                bool canAfford = events1.CheckFunds(shipment_price);
                if (!canAfford)
                {
                    return false;
                }
                events1.AddToStorage(prod_name, amount);
                events1.RecordProfit(shipment_price * (-1));
            }
            catch (Exception ex)
            {
                return false;
            }
            
            return true;
        }
    }
}
