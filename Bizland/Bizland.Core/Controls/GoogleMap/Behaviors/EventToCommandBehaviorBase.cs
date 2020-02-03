using Prism.Behaviors;

using System.Windows.Input;

using Xamarin.Forms;

namespace Bizland.Core.GoogleMap.Behaviors
{
    public abstract class EventToCommandBehaviorBase : BehaviorBase<Map>
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(MapClickedToCommandBehavior), default(ICommand));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public EventToCommandBehaviorBase()
        {
        }
    }
}