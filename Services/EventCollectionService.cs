using Plugin.Maui.Calendar.Models;
using MauiApp1.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class EventService : ObservableObject
    {
        private static EventService _instance;
        public static EventService Instance => _instance ??= new EventService();

        public EventCollection Events { get; set; }

        public event EventHandler EventsUpdated;
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

        public EventCollection GetEvents()
        {
            return Events;
        }
        public void SetEvents(EventCollection events)
        {
            Events = events;
        }
        public Dictionary<DateTime, List<EventModel>> GetEvents2()
        {
            Dictionary<DateTime, List<EventModel>> y = new Dictionary<DateTime, List<EventModel>>();
            foreach (var x in Events)
            {
                y.Add(x.Key, x.Value.Cast<EventModel>().ToList());
            }
            return y;
        }


        public void AddEvent(EventModel eventModel)
        {
            if (Events.ContainsKey(eventModel.DateTime))
            {
                List<EventModel> x = new List<EventModel>();
                x = (List<EventModel>)Events[eventModel.DateTime];

                x.Add(eventModel);
                Events.Remove(eventModel.DateTime);
                Events.Add(eventModel.DateTime, x);

            }
            else
            {
                Events[eventModel.DateTime] = new List<EventModel> { eventModel };
            }
        }

        public void UpdateEvent(EventModel eventModel)
        {
            if (Events.ContainsKey(eventModel.DateTime))
            {/*
                var eventList = Events[eventModel.DateTime];
                var existingEvent = eventList.Find(e => e.ID == eventModel.ID);
                if (existingEvent != null)
                {
                    existingEvent.Name = eventModel.Name;
                    existingEvent.Description = eventModel.Description;
                    existingEvent.DateTime = eventModel.DateTime;
                }
                */
            }
        }

        public void DeleteEvent(EventModel eventModel)
        {
            if (Events.ContainsKey(eventModel.DateTime))
            {
                // convert events of the day same as task into a list, iterate and find the specific item on that day
                List<EventModel> x = (List<EventModel>)Events[eventModel.DateTime];
                foreach (EventModel task in x)
                {
                    if (task.ID == eventModel.ID)
                    {
                        // remove that item from the list
                        x.Remove(task);
                        break;
                    }
                }
                // remove the tasks of the right day (cant remove just one task unfortunately/dont know)
                Events.Remove(eventModel.DateTime);
                // add the new modified list of tasks on the right day
                if (x.Count > 0) Events.Add(eventModel.DateTime, x);
            }
        }
    }
}

