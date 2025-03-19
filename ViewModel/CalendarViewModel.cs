using MauiApp1.Scripts;
using MauiApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Maui.Calendar.Models;
using CommunityToolkit.Maui.Views;
using MauiApp1.View;
using MauiApp1.Model;

namespace MauiApp1.ViewModel
{
    public partial class CalendarViewModel : ViewModelBase
    {
        public EventCollection Events => EventService.Instance.Events;

        // not necessary logically, but program doesn't run without it
        [ObservableProperty] private string _name;
        [ObservableProperty] private string _description;

        private readonly ClickEventBtn _clickEventBtn = new ClickEventBtn();

        private readonly CreateEventBtn _createEventBtn = new CreateEventBtn();

        public CalendarViewModel()
        {
            Initialize();
        }
        
        [RelayCommand]
        public void EventPressed(EventModel e)
        {
            _clickEventBtn.ShowPopup(ref e);
        }

        [RelayCommand]
        public void AddTask()
        {
            _createEventBtn.ShowPopup();
        }
    }
}
