using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class UserService
    {
        public static string UserID { get; private set; }

        private static bool wasSet = false;

        private static UserService _instance;
        public static UserService Instance => _instance ??= new UserService();

        private UserService() { }

        private UserService(string id)
        {
            if (!wasSet)
            {
                UserID = id;
                wasSet = true;
            }
        }

        public static void Initialize(string id)
        {
            if (!wasSet)
            {
                _instance = new UserService(id);
            }
            else if (UserID != id)
            {
                throw new Exception("User ID has already been set.");
            }
        }
    }
}
