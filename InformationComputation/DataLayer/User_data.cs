using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace DataLayer
{
    [Table(Name = "UserData")]
    public class User_data
    {
        static User_data()
        {
            DatabaseInitializer.Initialize();
        }

        public enum UserType
        {
            Customer,
            Supplier
        }

        [Column(IsPrimaryKey = true)]
        public long Id { get; set; }

        [Column]
        public int Type { get; set; }

        // Property to maintain backward compatibility
        public Dictionary<long, UserType> UserData
        {
            get
            {
                var data = new Dictionary<long, UserType>();
                using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
                {
                    var users = db.UserDatas.ToList();
                    foreach (var user in users)
                    {
                        data[user.Id] = (UserType)user.Type;
                    }
                }
                return data;
            }
        }

        public void AddUser(long id, UserType type)
        {
            if (id > 0)
            {
                using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
                {
                    var user = new User_data { Id = id, Type = (int)type };
                    db.UserDatas.InsertOnSubmit(user);
                    db.SubmitChanges();
                }
            }
        }

        public void RemoveUser(long id)
        {
            using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
            {
                var user = db.UserDatas.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    db.UserDatas.DeleteOnSubmit(user);
                    db.SubmitChanges();
                }
            }
        }

        public UserType GetUser(long id)
        {
            using (var db = new ShopDatabaseDataContext(DatabaseInitializer.GetConnectionString()))
            {
                var user = db.UserDatas.FirstOrDefault(u => u.Id == id);
                if (user == null)
                    throw new KeyNotFoundException($"User with ID {id} not found.");
                return (UserType)user.Type;
            }
        }
    }
}
