using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Scripts
{
    public class AuthConnection
    {
        private static AuthConnection _instance;
        public static AuthConnection Instance => _instance ??= new AuthConnection();

        public FirebaseAuthClient firebaseAuthClient { get; private set; }

        private AuthConnection()
        {
            firebaseAuthClient = new FirebaseAuthClient(new FirebaseAuthConfig() { 
                 ApiKey = "\r\nAIzaSyCpdHlPnyyfMPI7EMPmfAdN8sWE5icWZp0",
                 AuthDomain = "todo-maui-firebase.firebaseapp.com",
                 Providers = [new EmailProvider()]
            });
        }
    }
}
