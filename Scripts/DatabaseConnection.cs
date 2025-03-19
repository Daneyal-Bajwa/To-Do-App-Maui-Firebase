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
        public FirebaseClient connection;

        public void Connect()
        {
            connection = new FirebaseClient("https://todo-maui-firebase-default-rtdb.europe-west1.firebasedatabase.app/");
        }
    }
}
