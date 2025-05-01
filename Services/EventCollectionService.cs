using Plugin.Maui.Calendar.Models;
using MauiApp1.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using MauiApp1.Scripts;
using Firebase.Database.Query;
using System.Collections.Immutable;
using System;
using Firebase.Database.Streaming;
using Newtonsoft.Json;

namespace MauiApp1.Services
{
    public class EventService
    {
        private static EventService _instance;
        private static FirebaseClient _firebaseClient;

        public static EventService Instance => _instance ??= new EventService();
        private static string _userService => UserService.UserID;

        public EventCollection Events { get; set; }


        private EventService()
        {
            Events = new EventCollection();

            _firebaseClient = DatabaseConnection.Instance.firebaseClient;
            LoadDataAsync();
        }

        private static DateTime DecodeDateTime(string dateTimeString)
        {
            string format = "yyyy-MM-dd";
            DateTime dateTime = DateTime.ParseExact(dateTimeString, format, System.Globalization.CultureInfo.InvariantCulture);

            return dateTime;
        }
        private static string EncodeDateTime(DateTime dateTime)
        {
            string dateTimeString = dateTime.ToString("yyyy-MM-dd");
            return dateTimeString;
        }
        private async Task LoadDataAsync()
        {
            _firebaseClient.Child("Event Collection").Child(_userService).AsObservable<List<EventModel>>().Subscribe((item) =>
            {

                if (item.EventType == Firebase.Database.Streaming.FirebaseEventType.Delete)
                {
                    Console.WriteLine("Delete triggered");
                    // if item exists in events, it is removed (this is for when the only task of the day is cleared)
                    if (item.Object.Count>0) DeleteEvent(item.Object[0]);
                }
                else if (item.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                {
                    Console.WriteLine("Insert or update triggered");
                    if (item.Object != null)
                    {
                        DateTime dateKey = DecodeDateTime(item.Key);
                        Events[dateKey] = item.Object;
                    }
                }
            });

        }
        public void SortEvents()
        {
            var orderedEvents = Events.OrderBy(e => e.Key).ToList();

            Events.Clear();
            foreach (var item in orderedEvents)
            {
                Events[item.Key] = item.Value;
            }
        }

        public void AddEvent(EventModel eventModel)
        {
            if (Events.ContainsKey(eventModel.DateTime.Date))
            {
                List<EventModel> x = (List<EventModel>)Events[eventModel.DateTime];

                x.Add(eventModel);
                Events.Remove(eventModel.DateTime);
                Events.Add(eventModel.DateTime, x);
                string dateKey = eventModel.DateTime.ToString("yyyy-MM-dd");
                Add(dateKey, x);
            }
            else
            {
                Events[eventModel.DateTime] = new List<EventModel> { eventModel };
                string dateKey = eventModel.DateTime.ToString("yyyy-MM-dd");
                Add(dateKey, new List<EventModel> { eventModel });
            }
        }
        
        public void UpdateEvent(ref EventModel eventModel)
        {
            if (Events.ContainsKey(eventModel.DateTime.Date))
            {
                List<EventModel> x = (List<EventModel>)Events[eventModel.DateTime.Date];
                foreach (EventModel item in x)
                {
                    if (item.ID == eventModel.ID)
                    {
                        item.Name = eventModel.Name;
                        item.Description = eventModel.Description;
                        item.DateTime = eventModel.DateTime;
                        item.ReminderOption = eventModel.ReminderOption;
                        item.IsCompleted = eventModel.IsCompleted;

                        break;
                    }
                }
                Events[eventModel.DateTime.Date] = x;
                Add(EncodeDateTime(eventModel.DateTime), x);                   
            }
        }

        public void DeleteEvent(EventModel eventModel)
        {
            if (Events.ContainsKey(eventModel.DateTime.Date))
            {
                // convert events of the day same as task into a list, iterate and find the specific item on that day
                List<EventModel> x = (List<EventModel>)Events[eventModel.DateTime];
                bool removed = false;
                foreach (EventModel task in x)
                {
                    if (task.ID == eventModel.ID)
                    {
                        // remove that item from the list
                        x.Remove(task);
                        removed = true;
                        break;
                    }
                }
                // prevent feedback loop between db and this
                if (!removed) return;
                // remove the tasks of the right day (cant remove just one task unfortunately)
                Events.Remove(eventModel.DateTime);
                // add the new modified list of tasks on the right day
                string dateKey = EncodeDateTime(eventModel.DateTime);
                if (x.Count > 0)
                {
                    Events.Add(eventModel.DateTime, x);

                    Add(dateKey, x);
                }
                else
                {
                    _firebaseClient
                        .Child($"Event Collection")
                        .Child(_userService)
                        .Child(dateKey)
                        .DeleteAsync();
                }
            }
        }

        private async static Task Add(string dateKey, List<EventModel> eventModels)
        {
            await _firebaseClient
                .Child($"Event Collection")
                .Child(_userService)
                .Child(dateKey)
                .PutAsync(eventModels);
        }
    }
}

