using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiApp1.ViewModel
{
    public abstract class ViewModelBase : ObservableObject
    {
        // virtual functions can be overrridden in inherited classes
        public virtual void Initialize() { }
        // abstract functions must be implemented by inherited classes
        // public abstract void Navigate(string pageName);

    }
}