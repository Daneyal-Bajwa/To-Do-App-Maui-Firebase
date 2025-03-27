using CommunityToolkit.Maui.Views;
using MauiApp1.Services;
using MauiApp1.View;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MauiApp1.Model.ReminderOptionsModel;

namespace MauiApp1.Scripts
{
    public partial class ClickEventBtn : ObservableObject
    {
        public EventCollection Events => EventService.Instance.Events;

        [ObservableProperty] private List<ReminderOptions> _options = new List<ReminderOptions>();
        [ObservableProperty] private bool _isCheckBoxEnabled;

        private PopupPage popupPage;
        private EventModel _currSelectedEvent;

        // hard copy of event, stores details in case the user decides to cancel editing
        [ObservableProperty] EventModel _tempItem;

        [ObservableProperty] private TimeSpan _tempTime;

        public void ShowPopup(ref EventModel e)
        {
            TempTime = e.DateTime.TimeOfDay;
            IsCheckBoxEnabled = true;
            if (e.ReminderOption == ReminderOptions.None)
            {
                IsCheckBoxEnabled = false;
            }
            Options = new ReminderOptionsModel().GetReminderOptions();
            popupPage = new PopupPage();
            // pointer to the currently selected event item
            _currSelectedEvent = e;


            TempItem = new EventModel(e.Name, e.Description, e.DateTime);
            TempItem.ReminderOption = e.ReminderOption;


            popupPage.BindingContext = this; // Set the BindingContext
            Application.Current.MainPage.ShowPopup(popupPage);
        }

        [RelayCommand]
        // need to delete item from the list
        public void Delete()
        {
            EventService.Instance.DeleteEvent(_currSelectedEvent);
            popupPage.Close();
        }

        [RelayCommand]
        public void Close()
        {
            popupPage.Close();
        }

        // update the current event with the details of the temporary item we created
        [RelayCommand]
        public void Save()
        {
            _currSelectedEvent.Name = TempItem.Name;
            _currSelectedEvent.Description = TempItem.Description;
            _currSelectedEvent.DateTime = _currSelectedEvent.DateTime.Date + TempTime;
            // if the user unchecked the check box, that means they do not want a reminder
            if (!IsCheckBoxEnabled)
                _currSelectedEvent.ReminderOption = ReminderOptions.None;
            else
                _currSelectedEvent.ReminderOption = TempItem.ReminderOption;

            EventService.Instance.UpdateEvent(_currSelectedEvent);
            popupPage.Close();
        }

        [RelayCommand]
        public void ReminderChosen(ReminderOptions reminderOption)
        {
            TempItem.ReminderOption = reminderOption;
        }
    }
}
