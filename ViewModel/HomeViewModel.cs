using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using MauiApp1.Services;

namespace MauiApp1.ViewModel
{
    public partial class HomeViewModel : ViewModelBase
    {
        // adding service so state of sidebar is persistent
        private readonly ISidebarService sidebarService;
        private readonly INavigationService navigationService;
        
        [ObservableProperty]
        private string _text = "";

        [ObservableProperty]
        public ObservableCollection<string> _items = new();

        public VerticalStackLayout Sidebar { get; set; }

        public HomeViewModel()
        {
            sidebarService = new SidebarService();
            navigationService = new NavigationService();
            Items = new ObservableCollection<string>();
        }

        [RelayCommand]
        private void Add(string str)
        {
            if (!string.IsNullOrWhiteSpace(Text))
            {
                Items.Add(Text);
                Text = string.Empty;
            }
        }

        [RelayCommand]
        private void Delete(string item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
            }
        }

        [RelayCommand]
        public override void Navigate(string pageName)
        {
            navigationService.Navigate(pageName);
        }

        [RelayCommand]
        public override void ToggleSidebar()
        {
            sidebarService.ToggleSidebar(Sidebar);
        }
    }
}
