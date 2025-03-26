using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Scripts
{
    public class DatabaseConnection
    {
        private static DatabaseConnection _instance;
        public static DatabaseConnection Instance => _instance ??= new DatabaseConnection();

        public FirebaseClient firebaseClient { get; private set; }

        private DatabaseConnection()
        {
            firebaseClient = new FirebaseClient("https://todo-maui-firebase-default-rtdb.europe-west1.firebasedatabase.app/");
        }
    }
}
