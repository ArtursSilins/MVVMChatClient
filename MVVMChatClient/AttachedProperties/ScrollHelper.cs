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
    public class ScrollHelper : BaseAttachedProperty<ScrollHelper, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(sender))
                return;

            if (!(sender is ScrollViewer control))
                return;

            control.ScrollChanged -= AutoScrollPropertyChanged;
            control.ScrollChanged += AutoScrollPropertyChanged;
        }
        private static void AutoScrollPropertyChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;

            if (scrollViewer.ScrollableHeight - scrollViewer.VerticalOffset < 95)
            {
                scrollViewer.ScrollToBottom();
            }
        }
    }
}
