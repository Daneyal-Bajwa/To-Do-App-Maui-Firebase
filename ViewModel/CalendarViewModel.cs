using MauiApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    public partial class CalendarViewModel : ViewModelBase
    {
        [ObservableProperty]
        private Dictionary<DateTime, List<string>> _tasks;

        public CalendarViewModel()
        {
            Tasks = new Dictionary<DateTime, List<string>>
            {
                { DateTime.Now.Date, new List<string> { "Task 1", "Task 2" } },
                { DateTime.Now.Date.AddDays(1), new List<string> { "Task 3" } }
            };
        }

    }
}
