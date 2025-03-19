using Plugin.Maui.Calendar.Models;
using MauiApp1.Model;
using System.Collections.Generic;

namespace MauiApp1.Services
{
    public class EventService
    {
        private static EventService _instance;
        public static EventService Instance => _instance ??= new EventService();

        public EventCollection Events { get; private set; }

        private EventService()
        {
            Events = new EventCollection
            {
                [DateTime.Now] = new List<EventModel>
                {
                    new EventModel("Cool event1", "This is Cool event1's description!", DateTime.Now),
                    new EventModel("Cool event2", "This is Cool event2's description!", DateTime.Now)
                },
                [DateTime.Now.AddDays(5)] = new List<EventModel>
                {
                    new EventModel("Cool event3", "This is Cool event3's description!", DateTime.Now.AddDays(5)),
                    new EventModel("Cool event4", "This is Cool event4's description!", DateTime.Now.AddDays(5))
                },
                [DateTime.Now.AddDays(-3)] = new List<EventModel>
                {
                    new EventModel("Cool event5", "This is Cool event5's description!", DateTime.Now.AddDays(-3))
                },
                [new DateTime(2024, 3, 16)] = new List<EventModel>
                {
                    new EventModel("Cool event6", "This is Cool event6's description!", new DateTime(2024, 3, 16))
                }
            };
        }
    }
}
