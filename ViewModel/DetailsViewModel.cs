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
        private readonly INavigationService navigationService;
        public VerticalStackLayout Sidebar { get; set; }

        public DetailsViewModel()
        {
            sidebarService = new SidebarService();
            navigationService = new NavigationService();
        }

        [RelayCommand]
        private void Navigate(string pageName)
        {
            navigationService.Navigate(pageName);
        }

        [RelayCommand]
        private void ToggleSidebar()
        {
            sidebarService.ToggleSidebar(Sidebar);
        }
    }
}
