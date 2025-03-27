using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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

        private System.Timers.Timer _timer;
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
                        alertTime = DateTime.AddMilliseconds(5000);
                        SetAlert();
                        break;
                    case ReminderOptions.TwoHoursBefore:
                        alertTime = DateTime.AddHours(-2);
                        SetAlert();
                        break;
                    case ReminderOptions.OneDayBefore:
                        alertTime = DateTime.AddDays(-1);
                        SetAlert();
                        break;
                    case ReminderOptions.TwoDaysBefore:
                        alertTime = DateTime.AddDays(-2);
                        SetAlert();
                        break;
                }
            }
        }
        private DateTime alertTime;

        private void SetAlert()
        {
            TimeSpan timeToGo = alertTime - DateTime.Now;
            if (timeToGo <= TimeSpan.Zero)
            {
                // Time has already passed
                return;
            }

            _timer = new System.Timers.Timer(timeToGo.TotalMilliseconds);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = false; // Only trigger once
            _timer.Start();
        }
        private async void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            _timer.Dispose();

            // Display the alert
            await Application.Current.MainPage.DisplayAlert("Reminder", $"It's time for {this.Name}!", "Okay");
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
