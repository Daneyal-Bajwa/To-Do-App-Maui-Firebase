﻿using CommunityToolkit.Maui;
using Firebase.Database;
using MauiApp1.Services;
using MauiApp1.View;
using Microsoft.Extensions.Logging;

namespace MauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                // Initialize the .NET MAUI Community Toolkit by adding the below line of code
                .UseMauiCommunityToolkit()
                // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<HomeViewModel>();

            builder.Services.AddSingleton<DetailsPage>();
            builder.Services.AddSingleton<DetailsViewModel>();

            builder.Services.AddSingleton<CalendarPage>();
            builder.Services.AddSingleton<CalendarViewModel>();

            builder.Services.AddTransient<AddTaskPopupPage>();
            builder.Services.AddTransient<PopupPage>();


#if DEBUG
            builder.Logging.AddDebug();

#endif

            return builder.Build();
        }
    }
}
// conditional compile
// #if ANDROID
//      var button = new Android.Widget.Button();
// #elif
//      var button = new UIKit.UIButton();
// #endif
