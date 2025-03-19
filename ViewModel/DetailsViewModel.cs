using CommunityToolkit.Maui.Views;
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
        [ObservableProperty] string title;

        public EventModel TempItem2 { get; private set; }

        private AddTaskPopupPage addTaskPopupPage;
        private readonly UserData database;
        public EventCollection Events => EventService.Instance.Events;

        public DetailsViewModel()
        {
            database = new UserData();
            database.AddEvent(new EventModel(title, "test description", DateTime.Now));

        }

        [RelayCommand]
        public void AddTask()
        {
            TempItem2 = new EventModel("", "", DateTime.Now);
            addTaskPopupPage = new AddTaskPopupPage();
            Application.Current.MainPage.ShowPopup(addTaskPopupPage);

            // _addTaskPress.AddTask();


        }
    }
}
