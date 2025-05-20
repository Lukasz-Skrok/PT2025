using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public abstract class User_data
    {
        public enum UserType
        {
            Customer,
            Supplier
        }

        internal Dictionary<long, UserType> UserData = new Dictionary<long, UserType>();

        public void AddUser(long id, UserType type)
        {
            if (id > 0)
            {
                UserData.Add(id, type);
            }
        }
        public void RemoveUser(long id)
        {
            UserData.Remove(id);
        }
        public UserType GetUser(long id)
        {
            return UserData[id];
        }
    }
}
