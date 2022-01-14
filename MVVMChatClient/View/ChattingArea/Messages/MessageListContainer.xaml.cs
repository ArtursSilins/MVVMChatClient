using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVMChatClient
{
    /// <summary>
    /// Interaction logic for MessageListContainer.xaml
    /// </summary>
    public partial class MessageListContainer : UserControl
    {
        public MessageListContainer()
        {
            InitializeComponent();
            scroll.ScrollToBottom();
        }
        private void Scroll_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ScrollVerticalPreload.MouseDown = false;
            ScrollVerticalPreload.MouseWeel = false;
        }

        private void Scroll_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ScrollVerticalPreload.MouseDown = true;
            ScrollVerticalPreload.MouseDownCounter = 1;
            ScrollVerticalPreload.MouseWeel = false;
        }
        private void Scroll_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ScrollVerticalPreload.KeyDown = true;
            ScrollVerticalPreload.MouseDownCounter = 1;
            ScrollVerticalPreload.MouseWeel = false;
        }

        private void Scroll_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            ScrollVerticalPreload.KeyDown = false;
            ScrollVerticalPreload.MouseWeel = false;
        }

        private void Scroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollVerticalPreload.MouseWeel = true;
        }
    }
}
