using MauiApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Maui.Calendar.Models;
using CommunityToolkit.Maui.Views;
using MauiApp1.View;

namespace MauiApp1.ViewModel
{
    public partial class CalendarViewModel : ViewModelBase
    {
        public EventCollection Events { get; set; } = [];
        [ObservableProperty] private string _name;
        [ObservableProperty] private string _description;

        private readonly AddTaskPress _addTaskPress;

        public CalendarViewModel()
        {
            Initialize();
            _addTaskPress = new AddTaskPress();
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
                        new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!", DateTime = DateTime.Now },
                        new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!", DateTime = DateTime.Now }
                    },
                [DateTime.Now.AddDays(5)] = new List<EventModel>
                    {
                        new EventModel { Name = "Cool event3", Description = "This is Cool event3's description!", DateTime = DateTime.Now.AddDays(5) },
                        new EventModel { Name = "Cool event4", Description = "This is Cool event4's description!", DateTime = DateTime.Now.AddDays(5) }
                    },
                [DateTime.Now.AddDays(-3)] = new List<EventModel>
                    {
                        new EventModel { Name = "Cool event5", Description = "This is Cool event5's description!", DateTime = DateTime.Now.AddDays(-3) }
                    },
                [new DateTime(2024, 3, 16)] = new List<EventModel>
                    {
                        new EventModel { Name = "Cool event6", Description = "This is Cool event6's description!", DateTime = new DateTime(2024, 3, 16) }
                    }
            };


        }

        // need to show a new page that can be ridden of easily (modal page?)
        // can add task simply, and press '+' to add more details
        [RelayCommand]
        public void AddTaskPressed()
        {
            _addTaskPress.AddTask();

            List<EventModel> x = new List<EventModel>();
            try
            {
                x = (List<EventModel>)Events[DateTime.Now];
            }
            catch (KeyNotFoundException e)
            {

            }
            finally
            {
                x.Add(new EventModel { Name = "HII", Description = "LOLL" });
                Events.Remove(DateTime.Now);

                Events.Add(DateTime.Now, x);
            }
        }

        // need to show the details of the event, edit and save or delete

        [RelayCommand]
        public void EventPressed(EventModel e)
        {
            var popup = new PopupView();
            Application.Current.MainPage.ShowPopup(popup);
            // Events.Remove(e.DateTime);
            // throw new Exception(e.Name);
        }
        // need to delete item from the list
        public void EventSwiped(EventModel e)
        {
            throw new Exception();
            Events.Remove(e.DateTime);
        }
    }
    public class EventModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public DateTime DateTime { get; set; }
    }
}
