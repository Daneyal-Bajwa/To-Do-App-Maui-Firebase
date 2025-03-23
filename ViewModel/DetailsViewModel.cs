using CommunityToolkit.Maui.Views;
using MauiApp1.Scripts;
using MauiApp1.Services;
using MauiApp1.View;
using Plugin.Maui.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    public partial class DetailsViewModel : ViewModelBase
    {
        public EventCollection Events => EventService.Instance.Events;

        private readonly CreateEventBtn _createEventBtn;
        private readonly ClickEventBtn _clickEventBtn;

        private readonly UserData database;


        // not necessary logically, but program doesn't run without it
        [ObservableProperty] private string _key;
        [ObservableProperty] private string _value;

        [ObservableProperty] private string _name;
        [ObservableProperty] private string _description;


        public DetailsViewModel()
        {
            database = new UserData();
            // database.AddEvent(new EventModel(title, "test description", DateTime.Now));

            _createEventBtn = new CreateEventBtn();
            _clickEventBtn = new ClickEventBtn();

            foreach (var x in Events)
            {
                foreach (EventModel y in x.Value)
                {
                    Console.WriteLine(y.Name);
                }
            }

        }

        [RelayCommand]
        public void AddTask()
        {
            _createEventBtn.ShowPopup();
        }

        [RelayCommand]
        public void EventPressed(EventModel e)
        {
            _clickEventBtn.ShowPopup(ref e);
        }
    }
}