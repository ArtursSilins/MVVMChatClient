using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MVVMChatClient
{
    public class MonitorPasswordBoxProperty: BaseAttachedProperty<MonitorPasswordBoxProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var password = (sender as PasswordBox);

            if (!(sender is PasswordBox control))
                return;

            password.PasswordChanged -= Password_PasswordChanged;

            if ((bool)e.NewValue)
            {
                HasTextProperty.SetValue(password);
                password.PasswordChanged += Password_PasswordChanged;
            }
            
        }

        private static void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            HasTextProperty.SetValue((PasswordBox)sender);
        }

        public new static void SetValue(DependencyObject sender, bool value)
        {
            SetValue(sender, true);

        }

        public new static bool GetValue(DependencyObject sender)
        {
            return (bool)sender.GetValue(ValueProperty);
        }

    }

    public class HasTextProperty : BaseAttachedProperty<HasTextProperty, bool>
    {
        public static void SetValue(DependencyObject sender)
        {
            SetValue((PasswordBox)sender, ((PasswordBox)sender).SecurePassword.Length > 0);
        }

        public new static bool GetValue(DependencyObject sender)
        {
            return GetValue(sender);//(bool)sender.GetValue(ValueProperty);
        }
    }
}
