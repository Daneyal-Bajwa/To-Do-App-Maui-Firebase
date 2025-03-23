using Firebase.Database.Query;
using MauiApp1.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Model;
using Plugin.Maui.Calendar.Models;

namespace MauiApp1.Services
{
    public class UserData
    {
        private DatabaseConnection dbConnection = new DatabaseConnection();

        public EventModel test = new EventModel();

        public EventCollection Events => EventService.Instance.Events;

        //[ObservableProperty] public EventCollection events;

        public UserData() { 
            dbConnection.Connect();
            // Events2.GetEvents();
            LoadDataAsync();
        }

        public void AddEvent(EventModel eventModel)
        {
            dbConnection.connection.Child("Event").PostAsync(eventModel);
        }

        public async Task LoadDataAsync()
        {
            dbConnection.connection.Child("Event").AsObservable<EventCollection>().Subscribe((item) =>
            {
                if (item.Object != null)
                {
                    EventService.Instance.SetEvents(item.Object);
                }
            });
        }
    }
}
