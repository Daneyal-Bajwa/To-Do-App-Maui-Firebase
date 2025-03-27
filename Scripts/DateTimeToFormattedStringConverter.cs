using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MauiApp1.Converters
{
    public class DateTimeToFormattedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                if (dateTime.Year == DateTime.Now.Year)
                {
                    return dateTime.ToString("MMMM dd");
                }
                else
                {
                    return dateTime.ToString("MMMM dd, yyyy");
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
