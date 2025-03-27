using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MauiApp1.Model.ReminderOptionsModel;

namespace MauiApp1.Model
{
    public class EventModel
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime DateTime { get; set; }

        public bool IsCompleted { get; set; }

        private ReminderOptions _reminderOption;
        public ReminderOptions ReminderOption
        {
            get { return _reminderOption; }
            set
            {
                _reminderOption = value;
                switch (value)
                {
                    case ReminderOptions.None:
                        alertTime = DateTime.MaxValue; // no alert
                        break;
                    case ReminderOptions.OneHourBefore:
                        alertTime = DateTime.AddHours(-1);
                        break;
                    case ReminderOptions.TwoHoursBefore:
                        alertTime = DateTime.AddHours(-2);
                        break;
                    case ReminderOptions.OneDayBefore:
                        alertTime = DateTime.AddDays(-1);
                        break;
                    case ReminderOptions.TwoDaysBefore:
                        alertTime = DateTime.AddDays(-2);
                        break;
                }
            }
        }
        private DateTime alertTime;

        public EventModel(string Name, string Description, DateTime dateTime)
        {
            this.Name = Name;
            this.Description = Description;
            this.DateTime = dateTime;
            this.IsCompleted = false;
            this.ReminderOption = ReminderOptions.None;
        }
        public EventModel()
        {

        }
    }
}
