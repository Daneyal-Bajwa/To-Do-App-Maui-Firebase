using MauiApp1.Services;
using Plugin.Maui.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    public partial class RemindersViewModel :ViewModelBase
    {

        public EventCollection Events => EventService.Instance.Events;

        // not necessary logically, but program doesn't run without it
        [ObservableProperty] private string _key;
        [ObservableProperty] private string _value;

        [ObservableProperty] private string _name;
        [ObservableProperty] private string _description;
        [ObservableProperty] private string _isCompleted;
        public RemindersViewModel()
        {
            
        }
        public void SortEvents()
        {
            EventService.Instance.SortEvents();
        }
    }
}
