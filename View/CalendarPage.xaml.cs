namespace MauiApp1.View;

public partial class CalendarPage : ContentPage
{
	public CalendarPage()
	{
        InitializeComponent();
    }
    // Due to issues with maui controls disposing (memory leaks) it is recomended to call Dispose() method on page Unload event (or similiar)
    void UnloadedHandler(object sender, EventArgs e)
    {
        // crashes the app
        //if (Calendar != null) Calendar.Dispose();
    }
}