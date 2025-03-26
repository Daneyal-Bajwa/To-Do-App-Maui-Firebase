using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MauiApp1.Behaviors
{
    public class EntryCompletedBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EntryCompletedBehavior));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Completed += OnEntryCompleted;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Completed -= OnEntryCompleted;
        }

        private void OnEntryCompleted(object sender, EventArgs e)
        {
            if (Command?.CanExecute(null) ?? false)
            {
                Command.Execute(null);
            }
        }
    }
}
