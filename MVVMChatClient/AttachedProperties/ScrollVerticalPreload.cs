using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace MVVMChatClient
{
    public class ScrollVerticalPreload
    {
        public static readonly DependencyProperty MonitorScrollProperty =
            DependencyProperty.RegisterAttached("MonitorScroll", typeof(bool), typeof(ScrollVerticalPreload),
                new FrameworkPropertyMetadata(false, OnScrollChanged));

        private static bool FirstAddDone { get; set; }
        private bool LinesCountComplete { get; set; }
        /// <summary>
        /// Detect if scrollbar thumb was rolled down
        /// </summary>
        private static bool WasRollback { get; set; } 
        /// <summary>
        /// To ensure that the mouse down will execute only one time.
        /// </summary>
        public static int MouseDownCounter { get; set; }
        public static bool KeyDown { get; set; }
        public static bool MouseDown { get; set; }
        public static bool MouseWeel { get; internal set; }

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

            // Scroll to the bottom when writing or receiving new message scroll is at the bottom.
            if (scroll.ScrollableHeight - scroll.VerticalOffset < 95)
            {
                scroll.ScrollToBottom();
            }

            // Scroll down if the mouse button down and messages have been added.
            if (MouseDownCounter == 0 && MouseDown == true &&
                scroll.ScrollableHeight - scroll.VerticalOffset > scroll.ScrollableHeight * 0.5)
            {
                scroll.ScrollToVerticalOffset(scroll.VerticalOffset + scroll.ScrollableHeight * 0.2);
            }

            if (scroll.ScrollableHeight - scroll.VerticalOffset > scroll.ScrollableHeight * 0.5)
            {
                WasRollback = true;
            }

            if (scroll.ScrollableHeight - scroll.VerticalOffset > scroll.ScrollableHeight * 0.7
              && WasRollback && (MouseDownCounter == 1 || MouseWeel)
               && Core.Model.ChatSwitching.MessageAddControl.FinishAdd)
            {
                WasRollback = false;


                if (KeyDown == true)
                    MouseDownCounter = 1;
                else
                    MouseDownCounter = 0;

                Core.Model.ChatSwitching.MessageAddControl.FinishAdd = false;


                Core.Model.PublicChat.ChatListHolder.AddAdditionalMessages();

                if (KeyDown || MouseWeel)
                    scroll.ScrollToVerticalOffset(scroll.VerticalOffset + (scroll.ScrollableHeight * 0.5));

                MouseWeel = false;

            }

            //if (MVVMChatClient.Core.Model.CurrentChatMode.Mode == Core.Model.ChatMode.Public)
            //{

            //    if (scroll.ScrollableHeight - scroll.VerticalOffset > scroll.ScrollableHeight * 0.7 && FirstAddDone
            //         && !Core.Model.ChatSwitching.MessageAddControl.AllItemsAdded &&
            //         Core.Model.ChatSwitching.MessageAddControl.FinishAdd && WasRollback)
            //    {
            //        WasRollback = false;

            //        Core.Model.PublicChat.ChatListHolder.AddAdditionalMessages();


            //        //if(Core.Model.ChatSwitching.MessageAddControl.FinishAdd)

            //        Core.Model.ChatSwitching.MessageAddControl.FinishAdd = false;

            //        //scroll.IsDeferredScrollingEnabled = true;
            //        //scroll.IsManipulationEnabled = true;
            //    }

            //    //if (scroll.ScrollableHeight * 0.5 == scroll.VerticalOffset)
            //    //{
            //    //    isScrolledBack = true;
            //    //}

            //    FirstAddDone = true;
            //}
            //else if (MVVMChatClient.Core.Model.CurrentChatMode.Mode == Core.Model.ChatMode.Private)
            //{
            //    if (scroll.ScrollableHeight - scroll.VerticalOffset > scroll.ScrollableHeight * 0.7 && FirstAddDone
            //        && !Core.Model.ChatSwitching.MessageAddControl.AllItemsAdded &&
            //        Core.Model.ChatSwitching.MessageAddControl.FinishAdd)
            //    {
            //        Core.Model.PrivateChat.ChatListHolder.AddAdditionalMessages();
            //        Core.Model.ChatSwitching.MessageAddControl.FinishAdd = false;
            //    }

            //    FirstAddDone = true;
            //}
            //else
            //{

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
