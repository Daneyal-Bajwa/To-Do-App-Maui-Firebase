using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class NavigationService : INavigationService
    {
        public async void Navigate(string pageName)
        {
            switch (pageName)
            {
                case "HomePage":
                    await Shell.Current.GoToAsync("//HomePage");
                    break;
                case "DetailsPage":
                    await Shell.Current.GoToAsync(nameof(View.DetailsPage));
                    break;
            }
        }
    }
}
