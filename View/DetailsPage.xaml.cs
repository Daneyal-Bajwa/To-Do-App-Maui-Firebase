namespace MauiApp1.View;
using MauiApp1.ViewModel;

public partial class DetailsPage : ContentPage
{
    public DetailsPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var x = (DetailsViewModel)this.BindingContext;
        x.SortEvents();
        InitializeComponent();
    }
}
