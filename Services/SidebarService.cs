using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class SidebarService : ISidebarService
    {
        private bool isSidebarExpanded = true;

        public async void ToggleSidebar(VerticalStackLayout Sidebar)
        {
            if (Sidebar == null)
            {
                // Handle the case where the sidebar is not found
                Console.WriteLine("Sidebar not found");
                return;
            }

            if (isSidebarExpanded)
            {
                // Minimize the sidebar with fade-out effect
                await Sidebar.TranslateTo(-Sidebar.Width, 0, 500, Easing.Linear);
                await Sidebar.ScaleTo(0, 500, Easing.Linear);
                isSidebarExpanded = false;
            }
            else
            {
                await Sidebar.ScaleTo(1, 500, Easing.Linear);
                await Sidebar.TranslateTo(0, 0, 500, Easing.Linear);
                isSidebarExpanded = true;
            }
        }

    }
}
