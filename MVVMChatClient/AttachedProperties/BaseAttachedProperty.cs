using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMChatClient
{
    public class BaseAttachedProperty<Parent, Property> where Parent : BaseAttachedProperty<Parent, Property>, new()
    {

        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (s, e) => { };
        public static Parent Instance { get; private set; } = new Parent();

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached("Value", typeof(Property), typeof(BaseAttachedProperty<Parent, Property>), new PropertyMetadata(new PropertyChangedCallback(OnValuePropertyChange)));
        private static void OnValuePropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Instance.OnValueChanged(d, e);

            Instance.ValueChanged(d, e);
        }
        public static Property GetValue(DependencyObject d)
        {
            return (Property)d.GetValue(ValueProperty);
        }
        public static void SetValue(DependencyObject d, Property value)
        {
            d.SetValue(ValueProperty, value);
        }
        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }
    }
}
