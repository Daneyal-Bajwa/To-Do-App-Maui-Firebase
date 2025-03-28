using CommunityToolkit.Maui.Views;
using Firebase.Database;
using MauiApp1.Scripts;
using MauiApp1.Services;
using Plugin.Maui.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MauiApp1.ViewModel
{
    public partial class DetailsViewModel : ViewModelBase
    {
        public EventCollection Events => EventService.Instance.Events;

        private readonly CreateEventBtn _createEventBtn;
        private readonly ClickEventBtn _clickEventBtn;
        private readonly ClickSuggestTasksBtn _clickSuggestTaskBtn;

        // not necessary logically, but program doesn't run without it
        [ObservableProperty] private string _key;
        [ObservableProperty] private string _value;

        [ObservableProperty] private string _name;
        [ObservableProperty] private string _description;
        [ObservableProperty] private string _isCompleted;


        public DetailsViewModel()
        {
            _createEventBtn = new CreateEventBtn();
            _clickEventBtn = new ClickEventBtn();
            _clickSuggestTaskBtn = new ClickSuggestTasksBtn();

            List<EventModel> sendToReminder = new List<EventModel>();
            foreach (var item in Events)
            {
                foreach (EventModel eventModel in item.Value)
                {
                    sendToReminder.Add(eventModel);
                }
            }
            ReminderService reminderService = new ReminderService();
            reminderService.SetAlerts(sendToReminder);
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

        public void SortEvents()
        {
            EventService.Instance.SortEvents();
        }

        [RelayCommand]
        public void SuggestTasks()
        {
            _clickSuggestTaskBtn.ShowPopup();
        }
        [RelayCommand]
        public void UpdateEvent(EventModel eventModel)
        {
            EventService.Instance.UpdateEvent(ref eventModel);
        }

        [RelayCommand]
        public async void Refresh()
        {
            // need to implement yet
        }
    }
}