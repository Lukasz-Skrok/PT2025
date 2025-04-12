using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace LogicLayer
{
    public class RegisterUser
    {
        private readonly Events events;
        public RegisterUser(Events events)
        {
            this.events = events;
        }
        public bool CanPurchase(int id)
        {
            if (events.GetUser(id) == User_data.UserType.Customer)
            {
                return true;
            }
            return false;
        }

        public bool CanSupply(int id)
        {
            if (events.GetUser(id) == User_data.UserType.Supplier)
            {
                return true;
            }
            return false;
        }
    }
}
