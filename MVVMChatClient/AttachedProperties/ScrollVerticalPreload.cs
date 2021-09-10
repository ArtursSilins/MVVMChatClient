using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MVVMChatClient
{
    public class ScrollVerticalPreload
    {
        public static readonly DependencyProperty MonitorScrollProperty =
            DependencyProperty.RegisterAttached("MonitorScroll", typeof(bool), typeof(ScrollVerticalPreload),
                new FrameworkPropertyMetadata(false, OnScrollChanged));

        private static bool AllowedAddItems { get; set; }
        private static int OneScrollMaxWidth { get; set; } = 200;
        private static bool FirstAddDone { get; set; }
        private static void OnScrollChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ScrollViewer scroll))
                return;

            scroll.ScrollChanged -= Scroll_ScrollChanged;
            scroll.ScrollChanged += Scroll_ScrollChanged;
        }

        private static void Scroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scroll = sender as ScrollViewer;

            if(MVVMChatClient.Core.Model.CurrentChatMode.Mode == Core.Model.ChatMode.Public)
            {
                if (scroll.ScrollableHeight - scroll.VerticalOffset > scroll.ScrollableHeight*0.7 && FirstAddDone
                     && !Core.Model.ChatSwitching.ChatSwitchBase.AllItemsAdded &&
                     Core.Model.ChatSwitching.ChatSwitchBase.FinishAdd)
                {
                    //scroll.ScrollToVerticalOffset(scroll.ScrollableHeight * 0.6);
                    Core.Model.PublicChat.ChatListHolder.AddAdditionalMessages();
                    Core.Model.ChatSwitching.ChatSwitchBase.FinishAdd = false;
                }

                FirstAddDone = true;
            }
            else if (MVVMChatClient.Core.Model.CurrentChatMode.Mode == Core.Model.ChatMode.Private)
            {

            }


            //if (scroll.ScrollableHeight > scroll.VerticalOffset + 200 && !FirstAddDone)
            //{
            //    FirstAddDone = true;
            //    //LotteryCore.NoPatterns.Patterns.StopAdding = true;
            //    //LotteryCore.NoPatterns.Patterns.ButtonEnabled = true;
            //    //LotteryCore.NoPatterns.Patterns.IsEnded = true;
            //}
            //else if (scroll.ScrollableHeight - scroll.VerticalOffset < 300 &&
            //    FirstAddDone/* && !LotteryCore.NoPatterns.Patterns.AllItemsAdded*/ && !AllowedAddItems)
            //{
            //    AllowedAddItems = true;
            //    //LotteryCore.NoPatterns.Patterns.PopulatePatterns();
            //    OneScrollMaxWidth += 400;
            //}

            //if (scroll.ScrollableHeight > scroll.VerticalOffset + OneScrollMaxWidth && AllowedAddItems)
            //{
            //    AllowedAddItems = false;
            //    //LotteryCore.NoPatterns.Patterns.StopAdding = true;
            //}
        }

        public static void SetMonitorScroll(ScrollViewer scrollViewer, bool value)
        {
            scrollViewer.SetValue(MonitorScrollProperty, value);
        }
        private static bool GetMonitorScroll(ScrollViewer scrollViewer)
        {
            return true;
        }
    }
}
