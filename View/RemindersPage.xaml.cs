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
        //var x = (RemindersViewModel)this.BindingContext;
        //x.SortEvents();
        InitializeComponent();
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is EventModel eventModel)
        {
            var viewModel = BindingContext as RemindersViewModel;
            if (viewModel != null)
            {
                //viewModel.UpdateEventCommand.Execute(eventModel);
            }
        }
    }
}