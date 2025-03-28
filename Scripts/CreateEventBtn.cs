﻿using CommunityToolkit.Maui.Views;
using MauiApp1.Services;
using MauiApp1.View;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Scripts
{
    public partial class CreateEventBtn : ObservableObject
    {
        public EventCollection Events => EventService.Instance.Events;

        private AddTaskPopupPage addTaskPopupPage;
        [ObservableProperty] EventModel tempItem;
        // need to create this cuz DateTime.TimeOfDay is readonly, binding doesnt change it
        [ObservableProperty] TimeSpan _tempTime;

        public void ShowPopup()
        {
            TempItem = new EventModel("", "", DateTime.Today);
            TempTime = TempItem.DateTime.TimeOfDay;

            addTaskPopupPage = new AddTaskPopupPage();
            addTaskPopupPage.BindingContext = this; // Set the BindingContext
            Application.Current.MainPage.ShowPopup(addTaskPopupPage);
        }

        [RelayCommand]
        public void SimpleClose()
        {
            if (addTaskPopupPage != null) addTaskPopupPage.Close();
        }

        [RelayCommand]
        public void CreateTask()
        {
            if (TempItem != null && !string.IsNullOrEmpty(TempItem.Name))
            {
                TempItem.DateTime = TempItem.DateTime.Date + TempTime;
                EventService.Instance.AddEvent(TempItem);
                addTaskPopupPage.Close();                
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Event Creation Failed", "The Task Name is required to create an event.", "Okay");
            }
        }
    }
}
