using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Model
{
    public class ReminderOptionsModel
    {
        public enum ReminderOptions
        {
            None,
            [Description("One hour before")]
            OneHourBefore,
            [Description("Two hours before")]
            TwoHoursBefore,
            [Description("One day before")]
            OneDayBefore,
            [Description("Two days before")]
            TwoDaysBefore,
        }

        public List<ReminderOptions> GetReminderOptions()
        {
            return Enum.GetValues(typeof(ReminderOptions)).Cast<ReminderOptions>().ToList();
        }

        public string ToString(ReminderOptions option)
        {
            return option switch
            {
                ReminderOptions.OneHourBefore => "One hour before",
                ReminderOptions.TwoHoursBefore => "Two hours before",
                ReminderOptions.OneDayBefore => "One day before",
                ReminderOptions.TwoDaysBefore => "Two days before",
                _ => "N/A"
            };
        }
    }
}
