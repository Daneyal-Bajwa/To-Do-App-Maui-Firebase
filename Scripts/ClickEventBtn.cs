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

namespace MauiApp1.Scripts
{
    public partial class ClickEventBtn : ObservableObject
    {

        public EventCollection Events => EventService.Instance.Events;

        private PopupPage popupPage;
        [ObservableProperty] public EventModel currSelectedEvent;
        private EventModel tempItem;
        public void ShowPopup(ref EventModel e)
        {
            popupPage = new PopupPage();
            // pointer to the currently selected event item
            CurrSelectedEvent = e;
            // hard copy of item, stores details in case the user decides to cancel editing
            tempItem = new EventModel(e.Name, e.Description, e.DateTime);
            popupPage.BindingContext = this; // Set the BindingContext
            Application.Current.MainPage.ShowPopup(popupPage);
        }

        [RelayCommand]
        // need to delete item from the list
        public void Delete(EventModel task)
        {
            EventService.Instance.DeleteEvent(task);
            popupPage.Close();
        }

        // rather than saving upon pressing "Save", we instead undo the changes upon pressing "Cancel(or equivalent)"
        // this is to work around the gimmick of Binding and Observable Properties
        [RelayCommand]
        public void Close()
        {
            CurrSelectedEvent.Name = tempItem.Name;
            CurrSelectedEvent.Description = tempItem.Description;
            CurrSelectedEvent.DateTime = tempItem.DateTime;
            popupPage.Close();
        }

        // due to binding, data is already saved as the user inputs it
        // so don't need to do anything
        [RelayCommand]
        public void Save(EventModel task)
        {
            popupPage.Close();
        }
    }
}
