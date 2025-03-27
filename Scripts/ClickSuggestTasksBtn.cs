using CommunityToolkit.Maui.Views;
using MauiApp1.Services;
using MauiApp1.View;
using Plugin.Maui.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Scripts
{
    public partial class ClickSuggestTasksBtn : ViewModelBase
    {

        private SuggestiveTasksPopUpPage popupPage;
        [ObservableProperty] List<EventModel> _suggestions;

        public EventCollection Events => EventService.Instance.Events;

        public void ShowPopup()
        {
            Suggestions = new TaskSuggester().SuggestTasks();

            popupPage = new SuggestiveTasksPopUpPage();
            popupPage.BindingContext = this; // Set the BindingContext
            Application.Current.MainPage.ShowPopup(popupPage);
        }

        [RelayCommand]
        public void Close()
        {
            popupPage.Close();
        }


        [RelayCommand]
        public void Add(EventModel task)
        {
            EventService.Instance.AddEvent(task);
            Suggestions.Remove(task);
            // update the pop up by removing the selected item and leaving the rest
            popupPage.Close();
            popupPage = new SuggestiveTasksPopUpPage();
            popupPage.BindingContext = this; // Set the BindingContext
            Application.Current.MainPage.ShowPopup(popupPage);
        }
    }
}
