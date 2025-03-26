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

        public RemindersViewModel()
        {
            
        }
        public void SortEvents()
        {
            EventService.Instance.SortEvents();
        }
    }
}
