using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MVVMChatClient
{
    public class PasswordBoxProperties
    {
        public static readonly DependencyProperty MonitorPasswordProperty =
            DependencyProperty.RegisterAttached("MonitorPassword", typeof(bool), typeof(PasswordBoxProperties), new PropertyMetadata(false, OnMonitorPasswordChanged));

        private static void OnMonitorPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var password = (d as PasswordBox);

            if (!(d is PasswordBox control))
                return;

            password.PasswordChanged -= Password_PasswordChanged;

            if ((bool)e.NewValue)
            {
                SetHasText(password);
                password.PasswordChanged += Password_PasswordChanged;
            }
        }

        private static void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            SetHasText((PasswordBox)sender);
        }

        public static void SetMonitorPassword(PasswordBox passwordBox, bool value)
        {
            passwordBox.SetValue(MonitorPasswordProperty, value);
        }
        public static bool GetMonitorPassword(PasswordBox passwordBox)
        {
            return (bool)passwordBox.GetValue(MonitorPasswordProperty);
        }


        public static readonly DependencyProperty HasTextProperty =
           DependencyProperty.RegisterAttached("HasText", typeof(bool), typeof(PasswordBoxProperties), new PropertyMetadata(false));

        public static void SetHasText(PasswordBox passwordBox)
        {
            passwordBox.SetValue(HasTextProperty, passwordBox.SecurePassword.Length > 0);
        }
        public static bool GetHasText(PasswordBox passwordBox)
        {
            return (bool)passwordBox.GetValue(HasTextProperty);
        }

    }
}
