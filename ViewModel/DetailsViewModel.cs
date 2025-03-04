using MauiApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    public partial class DetailsViewModel : ObservableObject
    {
        // adding service so state of sidebar is persistent
        private readonly ISidebarService sidebarService;
        public VerticalStackLayout Sidebar { get; set; }

        public DetailsViewModel()
        {
            sidebarService = new SidebarService();
        }

        [RelayCommand]
        private async void Navigate(string pageName)
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

        [RelayCommand]
        private void ToggleSidebar()
        {
            sidebarService.ToggleSidebar(Sidebar);
        }
    }
}
