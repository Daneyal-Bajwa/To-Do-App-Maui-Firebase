using MauiApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Maui.Calendar.Models;

namespace MauiApp1.ViewModel
{
    public partial class CalendarViewModel : ViewModelBase
    {
        public EventCollection Events { get; set; } = [];
        [ObservableProperty] string _name;
        [ObservableProperty] string _description;

        public CalendarViewModel()
        {
            Initialize();
            /*
            Tasks = new Dictionary<DateTime, List<string>>
            {
                { DateTime.Now.Date, new List<string> { "Task 1", "Task 2" } },
                { DateTime.Now.Date.AddDays(1), new List<string> { "Task 3" } }
            };
            */
            Events = new EventCollection
            {
                [DateTime.Now] = new List<EventModel>
                    {
                        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
                        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" }
                    },
                [DateTime.Now.AddDays(5)] = new List<EventModel>
                    {
                        new EventModel { Name = "Cool event3", Description = "This is Cool event3's description!" },
                        new EventModel { Name = "Cool event4", Description = "This is Cool event4's description!" }
                    },
                [DateTime.Now.AddDays(-3)] = new List<EventModel>
                    {
                        new EventModel { Name = "Cool event5", Description = "This is Cool event5's description!" }
                    },
                [new DateTime(2024, 3, 16)] = new List<EventModel>
                    {
                        new EventModel { Name = "Cool event6", Description = "This is Cool event6's description!" }
                    }
            };
        }
    }
    internal class EventModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
