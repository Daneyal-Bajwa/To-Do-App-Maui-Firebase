using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Model
{
    public class EventModel
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime DateTime { get; set; }

        public EventModel(string Name, string Description, DateTime dateTime)
        {
            this.Name = Name;
            this.Description = Description;
            this.DateTime = dateTime;
        }
        public EventModel()
        {

        }
    }
}
