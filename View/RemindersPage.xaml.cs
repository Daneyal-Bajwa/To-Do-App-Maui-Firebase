namespace MauiApp1.View;

public partial class RemindersPage : ContentPage
{
	public RemindersPage()
	{
		InitializeComponent();
	}
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var x = (RemindersViewModel)this.BindingContext;
        x.SortEvents();
        InitializeComponent();
    }
}