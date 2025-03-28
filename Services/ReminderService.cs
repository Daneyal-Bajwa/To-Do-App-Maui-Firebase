using CommunityToolkit.Maui.Views;
using MauiApp1.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MauiApp1.Services
{
    public partial class ReminderService : ViewModelBase, IDisposable
    {

        // reminder stuff
        public static Dictionary<EventModel, System.Timers.Timer> Timers => _timers;
        private static Dictionary<EventModel, System.Timers.Timer> _timers = new Dictionary<EventModel, System.Timers.Timer>();

        private AlertPopupPage alertPopupPage;

        [ObservableProperty] private EventModel _currAlertEM;
        [ObservableProperty] private TimeSpan _timeLeft;

        public void SetAlerts(List<EventModel> eventModels)
        {
            foreach (EventModel item in eventModels)
                SetAlert(item);
        }
        public void SetAlert(EventModel eventModel)
        {
            // if there was previously a timer for this event and the event no longer has reminder
            if (_timers.ContainsKey(eventModel) && eventModel.ReminderOption == ReminderOptionsModel.ReminderOptions.None)
            {
                // remove the timer if the event has no reminder
                _timers[eventModel].Stop();
                _timers[eventModel].Dispose();
                _timers.Remove(eventModel);
                return;
            }

            TimeSpan timeToGo = eventModel.AlertTime - DateTime.Now;
            // if the event has a reminder and it hasn't been completed
            // and the time to go is greater than 0
            if (timeToGo <= TimeSpan.Zero
                || eventModel.IsCompleted
                || eventModel.ReminderOption == ReminderOptionsModel.ReminderOptions.None)
            {
                // invalid event to set an alert for
                return;
            }

            System.Timers.Timer _timer;

            _timer = new System.Timers.Timer(timeToGo.TotalMilliseconds);
            _timer.Elapsed += (sender, e) => OnTimedEvent(sender, e, eventModel);
            _timer.AutoReset = false; // Only trigger once
            _timer.Start();

            if (!_timers.TryAdd(eventModel, _timer))
            {
                // stop the old timer and dispose of it
                _timers[eventModel].Stop();
                _timers[eventModel].Dispose();
                _timers[eventModel] = _timer;
            }
        }
        private async void OnTimedEvent(object sender, ElapsedEventArgs e, EventModel eventModel)
        {
            if (sender is System.Timers.Timer timer)
            {
                timer.Stop();
                timer.Dispose();

                CurrAlertEM = eventModel;
                TimeLeft = eventModel.DateTime - DateTime.Now;
                if (TimeLeft <= TimeSpan.Zero)
                    TimeLeft = TimeSpan.Zero;

                // display the alert on the main UI thread so it can retrieve display size too adjust its own size
                Device.BeginInvokeOnMainThread(() =>
                {
                    alertPopupPage = new AlertPopupPage();
                    alertPopupPage.BindingContext = this; // Set the BindingContext
                    Application.Current.MainPage.ShowPopup(alertPopupPage);
                });
            }
        }

        [RelayCommand]
        public void Close()
        {
            alertPopupPage.Close();
        }

        public void Dispose()
        {
            foreach (System.Timers.Timer timer in _timers.Values)
            {
                timer.Stop();
                timer.Dispose();
            }
            _timers.Clear();
        }
    }
}
