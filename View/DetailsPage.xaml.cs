namespace MauiApp1.View;

using MauiApp1.Scripts;
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
        var suggestor = new TaskSuggester();
        suggestor.SuggestTasks();
    }


    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && checkBox.BindingContext is EventModel eventModel)
        {
            var viewModel = BindingContext as DetailsViewModel;
            if (viewModel != null)
            {
                viewModel.UpdateEvent(eventModel);
            }
        }
    }

    private void OnEventTapped(object sender, EventArgs e)
    {
        if (sender is Element element && element.BindingContext is EventModel eventModel)
        {
            var viewModel = BindingContext as DetailsViewModel;
            if (viewModel != null)
            {
                viewModel.EventPressed(eventModel);
            }
        }
    }
}
