using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MauiApp1.Controls
{
    public class CalendarView : ContentView
    {
        // layout holding the days of the month
        private readonly Grid _calendarGrid;
        // display curr month + year
        private readonly Label _monthLabel;
        // represent each day on calendar
        private readonly List<CalendarDay> _days;

        // holds dictionary of tasks for each date
        public static readonly BindableProperty TasksProperty =
            BindableProperty.Create(nameof(Tasks), typeof(Dictionary<DateTime, List<string>>), 
                typeof(CalendarView), new Dictionary<DateTime, List<string>>(), propertyChanged: OnTasksChanged);

        // get or set tasks
        public Dictionary<DateTime, List<string>> Tasks
        {
            get => (Dictionary<DateTime, List<string>>)GetValue(TasksProperty);
            set => SetValue(TasksProperty, value);
        }

        // initialise variables and layout
        public CalendarView()
        {
            _calendarGrid = new Grid
            {
                RowSpacing = 50,
                ColumnSpacing = 100,
                BackgroundColor = Colors.LightGray
            };

            for (int i = 0; i < 7; i++)
            {
                _calendarGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }

            for (int i = 0; i < 6; i++)
            {
                _calendarGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            }

            _monthLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 24,
                TextColor = Colors.Black
            };

            _days = new List<CalendarDay>();

            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    var day = new CalendarDay();
                    Grid.SetColumn(day, col);
                    Grid.SetRow(day, row);
                    _calendarGrid.Children.Add(day);
                    _days.Add(day);
                }
            }

            var stackLayout = new StackLayout
            {
                Children = { _monthLabel, _calendarGrid }
            };

            Content = stackLayout;

            UpdateCalendar(DateTime.Now);
        }

        // update calendar when tasks change
        private static void OnTasksChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CalendarView calendarView)
            {
                calendarView.UpdateCalendar(DateTime.Now);
            }
        }

        // update calendar when month changes
        private void UpdateCalendar(DateTime date)
        {
            _monthLabel.Text = date.ToString("MMMM yyyy");

            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var firstDayOfWeek = firstDayOfMonth.AddDays(-(int)firstDayOfMonth.DayOfWeek);

            for (int i = 0; i < _days.Count; i++)
            {
                var day = _days[i];
                var currentDate = firstDayOfWeek.AddDays(i);
                day.Date = currentDate;
                day.Tasks = Tasks.ContainsKey(currentDate) ? Tasks[currentDate] : new List<string>();
            }
        }
    }

    public class CalendarDay : ContentView
    {
        private readonly Label _dateLabel;
        private readonly StackLayout _tasksLayout;

        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        public static readonly BindableProperty DateProperty =
            BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(CalendarDay), default(DateTime), propertyChanged: OnDateChanged);

        public List<string> Tasks
        {
            get => (List<string>)GetValue(TasksProperty);
            set => SetValue(TasksProperty, value);
        }

        public static readonly BindableProperty TasksProperty =
            BindableProperty.Create(nameof(Tasks), typeof(List<string>), typeof(CalendarDay), new List<string>(), propertyChanged: OnTasksChanged);

        public CalendarDay()
        {
            _dateLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                FontSize = 14,
                TextColor = Colors.Black
            };

            _tasksLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var stackLayout = new StackLayout
            {
                Children = { _dateLabel, _tasksLayout }
            };

            Content = stackLayout;
        }

        private static void OnDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CalendarDay calendarDay)
            {
                calendarDay._dateLabel.Text = ((DateTime)newValue).Day.ToString();
            }
        }

        private static void OnTasksChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CalendarDay calendarDay)
            {
                calendarDay._tasksLayout.Children.Clear();
                foreach (var task in (List<string>)newValue)
                {
                    calendarDay._tasksLayout.Children.Add(new Label { Text = task, FontSize = 12, TextColor = Colors.Gray });
                }
            }
        }
    }
}

