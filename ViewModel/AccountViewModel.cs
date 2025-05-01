using MauiApp1.Services;
using Microcharts;
using Plugin.Maui.Calendar.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    public partial class AccountViewModel : ViewModelBase
    {
        public static EventCollection Events => EventService.Instance.Events;

        [ObservableProperty] private PieChart _test;
        [ObservableProperty] private int _doneCount = 0;
        [ObservableProperty] private int _dueCount = 0;
        [ObservableProperty] private int _overdueCount = 0;

        public AccountViewModel()
        {
            LoadTaskStatistics();
            DrawPieChart();
        }

        private void LoadTaskStatistics()
        {

            foreach (var x in Events)
            {
                foreach (EventModel y in x.Value)
                {
                    if (y.IsCompleted)
                    {
                        DoneCount++;
                    }
                    else
                    {
                        if (y.DateTime > DateTime.Now)
                            DueCount++;
                        else
                            OverdueCount++;
                    }
                }
            }
        }
        private void DrawPieChart()
        {
            Test = new PieChart
            {
                Entries = new List<ChartEntry>
                {
                    new ChartEntry(DoneCount) { Label = "Done", ValueLabel = DoneCount.ToString(), Color = SKColor.Parse("#0000FF") },
                    new ChartEntry(DueCount) { Label = "Due", ValueLabel = DueCount.ToString(), Color = SKColor.Parse("#FFFF00") },
                    new ChartEntry(OverdueCount) { Label = "Overdue", ValueLabel = OverdueCount.ToString(), Color = SKColor.Parse("#FF0000") }
                },
                BackgroundColor = SKColors.Transparent
            };

        }

    }

}
