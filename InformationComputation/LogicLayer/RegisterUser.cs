using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using UserType = DataLayer.User_data.UserType;

namespace LogicLayer
{
    public abstract class RegisterUser
    {
        protected readonly Events events;

        public RegisterUser(Events events)
        {
            this.events = events;
        }

        public abstract bool CanPurchase(long id);
        public abstract bool CanSupply(long id);
        public abstract void AddUser(long id, UserType userType);
    }

    public class RegisterUserLogic : RegisterUser
    {
        public RegisterUserLogic(Events events) : base(events) { }

        public override bool CanPurchase(long id)
        {
            return events.GetUser(id) == User_data.UserType.Customer;
        }

        public override bool CanSupply(long id)
        {
            return events.GetUser(id) == User_data.UserType.Supplier;
        }

        public override void AddUser(long id, UserType userType)
        {
            // Map LogicLayer.UserType → DataLayer.User_data.UserType
            var internalType = userType switch
            {
                UserType.Customer => User_data.UserType.Customer,
                UserType.Supplier => User_data.UserType.Supplier,
                _ => throw new ArgumentOutOfRangeException()
            };

            events.AddUser(id, internalType);
        }
    }
}
