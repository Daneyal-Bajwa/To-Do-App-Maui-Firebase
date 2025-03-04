namespace MauiApp1.View;
using MauiApp1.ViewModel;

public partial class HomePage : ContentPage
{
    public HomePage(HomeViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        var viewModel = (HomeViewModel)BindingContext;
        viewModel.Sidebar = Sidebar;
    }
}
