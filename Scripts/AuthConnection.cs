using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Scripts
{
    public class AuthConnection
    {

        public FirebaseAuthClient firebaseAuthClient { get; private set; }

        public AuthConnection(IConfiguration configuration)
        {
            firebaseAuthClient = new FirebaseAuthClient(new FirebaseAuthConfig() { 
                 ApiKey = ${{secrets.API_Key}},
                 AuthDomain = "todo-maui-firebase.firebaseapp.com",
                 Providers = [new EmailProvider()]
            });
        }
    }
}
