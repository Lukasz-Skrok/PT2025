using System.Diagnostics;

namespace DataLayer
{
    internal class Users_class
    {
        private Dictionary<int, string> users = new();

        public void Add2Users(int ID, string type)
        {
            users.Add(ID, type);
        }

        public string FindUser(int id)
        {
            return users[id];
        }

        public Dictionary<int, string> GetAllUsers()
        {
            return users;
        }
    }
}
