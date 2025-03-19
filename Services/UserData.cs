using Firebase.Database.Query;
using MauiApp1.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Model;

namespace MauiApp1.Services
{
    public class UserData
    {
        private DatabaseConnection dbConnection = new DatabaseConnection();

        public EventModel test = new EventModel();

        public ObservableCollection<EventModel> eventList { get; set; } = new ObservableCollection<EventModel>();

        public UserData() { 
            dbConnection.Connect();
            LoadDataAsync();
        }

        public void AddEvent(EventModel eventModel)
        {
            dbConnection.connection.Child("Event").PostAsync(eventModel);
        }

        public async Task LoadDataAsync()
        {
            dbConnection.connection.Child("Event").AsObservable<EventModel>().Subscribe((item) =>
            {
                if (item.Object != null)
                {
                    eventList.Add(item.Object);
                }
            });
        }
    }
}
