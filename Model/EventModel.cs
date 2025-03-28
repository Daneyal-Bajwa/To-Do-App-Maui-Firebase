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

        public DateTime AlertTime { get; private set; }

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
                        AlertTime = DateTime.MaxValue; // no alert
                        break;
                    case ReminderOptions.OnTime:
                        AlertTime = DateTime;
                        break;
                    case ReminderOptions.OneHourBefore:
                        AlertTime = DateTime.AddHours(-1);
                        break;
                    case ReminderOptions.TwoHoursBefore:
                        AlertTime = DateTime.AddHours(-2);
                        break;
                    case ReminderOptions.OneDayBefore:
                        AlertTime = DateTime.AddDays(-1);
                        break;
                    case ReminderOptions.TwoDaysBefore:
                        AlertTime = DateTime.AddDays(-2);
                        break;
                }
            }
        }

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
            this.IsCompleted = false;
            this.ReminderOption = ReminderOptions.None;
        }

    }
}
