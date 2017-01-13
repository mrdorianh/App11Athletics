using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ToolkitXam
{
    public class ToggleBase : ContentView
    {
        public event EventHandler<ToggledEventArgs> Toggled;

        public static readonly BindableProperty IsToggledProperty =
            BindableProperty.Create("IsToggled", typeof(bool), typeof(ToggleBase), false,
                                    BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    EventHandler<ToggledEventArgs> handler = ((ToggleBase)bindable).Toggled;
                    if (handler != null)
                        handler(bindable, new ToggledEventArgs((bool)newValue));
                });

        public ToggleBase()
        {
            ToggleBehavior toggleBehavior = new ToggleBehavior();
            toggleBehavior.PropertyChanged += OnToggleBehaviorPropertyChanged;
            Behaviors.Add(toggleBehavior);
        }

        public bool IsToggled
        {
            set { SetValue(IsToggledProperty, value); }
            get { return (bool)GetValue(IsToggledProperty); }
        }

        protected void OnToggleBehaviorPropertyChanged(object sender, 
                                                       PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "IsToggled")
            {
                IsToggled = ((ToggleBehavior)sender).IsToggled;
            }
        }
    }
}
