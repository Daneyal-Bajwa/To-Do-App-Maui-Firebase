using CommunityToolkit.Maui.Views;

namespace MauiApp1.View;

public partial class AddTaskPopupPage : Popup
{
	public AddTaskPopupPage()
	{
		InitializeComponent();
        Size = new Size(DeviceDisplay.MainDisplayInfo.Width / 3, DeviceDisplay.MainDisplayInfo.Height / 4);
    }
}