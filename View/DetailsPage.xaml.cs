namespace MauiApp1.View;
using MauiApp1.ViewModel;

public partial class DetailsPage : ContentPage
{
    public DetailsPage(DetailsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        var viewModel = (DetailsViewModel)BindingContext;
        viewModel.Sidebar = Sidebar;
    }

}
