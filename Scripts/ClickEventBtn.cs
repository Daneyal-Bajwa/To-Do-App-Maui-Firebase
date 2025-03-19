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
            try
            {
                // convert events of the day same as task into a list, iterate and find the specific item on that day
                List<EventModel> x = (List<EventModel>)Events[task.DateTime];
                foreach (EventModel model in x)
                {
                    if (model.ID == task.ID)
                    {
                        // remove that item from the list
                        x.Remove(model);
                        break;
                    }
                }
                // remove the tasks of the right day (cant remove just one task unfortunately/dont know)
                Events.Remove(task.DateTime);
                // add the new modified list of tasks on the right day
                if (x.Count > 0) Events.Add(task.DateTime, x);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("bruh. task(argument) was empty -_-", e);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException("bruh. this item doesn't exist " + task.Name, e);
            }
            finally
            {
                popupPage.Close();
            }
        }

        [RelayCommand]
        public void Close()
        {
            CurrSelectedEvent.Name = tempItem.Name;
            CurrSelectedEvent.Description = tempItem.Description;
            CurrSelectedEvent.DateTime = tempItem.DateTime;
            popupPage.Close();
        }

        [RelayCommand]
        public void Save(EventModel task)
        {
            popupPage.Close();
        }
    }
}
