using CommunityToolkit.Maui.Views;

namespace MauiApp1.View;

public partial class AlertPopupPage : Popup
{
    public AlertPopupPage()
    {
        InitializeComponent();

        Size = new Size(DeviceDisplay.MainDisplayInfo.Width / 3, DeviceDisplay.MainDisplayInfo.Height / 3.5);
    }

}
