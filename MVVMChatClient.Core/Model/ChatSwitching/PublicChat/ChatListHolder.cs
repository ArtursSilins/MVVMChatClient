using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model.ChatSwitching;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MVVMChatClient.Core.Model.PublicChat
{
    public class ChatListHolder:MessageAddControl
    {
        public static ObservableCollection<IMessageContent> Content { get; set; } = 
            new ObservableCollection<IMessageContent>();
        /// <summary>
        /// Add the list of messages from the server on the first connection. (Main thread with DispatcherTimer)
        /// </summary>
        public static void AddMessagesFirstTime()
        {
            WaitTimer = new DispatcherTimer();
            WaitTimer = new DispatcherTimer(DispatcherPriority.SystemIdle);
            WaitTimer.Interval = TimeSpan.FromMilliseconds(400);
            WaitTimer.Tick += WaitTimer_Tick;
            WaitTimer.Start();

        }

        private static void WaitTimer_Tick(object sender, EventArgs e)
        {
            AddTimer = new DispatcherTimer();
            AddTimer = new DispatcherTimer(DispatcherPriority.SystemIdle);
            AddTimer.Interval = TimeSpan.FromMilliseconds(5);
            AddTimer.Tick += AddTimer_Tick;
            AddTimer.Start();

            WaitTimer.Stop();

        }
        /// <summary>
        /// Add last 30 messages to view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AddTimer_Tick(object sender, EventArgs e)
        {
            AddFirstTimeMessages(Content);
        }
        /// <summary>
        /// Add the list of messages from the server on the first connection. (In separate thread)
        /// </summary>
        /// <param name="uiContext"></param>
        public static void AddFirstTimeMessages(SynchronizationContext uiContext)
        {
           int counter = 0;
            while (counter<21)
            {
                AddFirstTimeMessages(Content, uiContext);
                counter++;
            }
        }
        /// <summary>
        /// Add messages to public chat list.
        /// </summary>
        public static void AddAdditionalMessages()
        {
            AddAdditionalTimer = new DispatcherTimer();
            AddAdditionalTimer = new DispatcherTimer(DispatcherPriority.SystemIdle);
            AddAdditionalTimer.Interval = TimeSpan.FromMilliseconds(5);
            AddAdditionalTimer.Tick -= AddAdditionalTimer_Tick;
            AddAdditionalTimer.Tick += AddAdditionalTimer_Tick;
            AddAdditionalTimer.Start();
        }

        private static void AddAdditionalTimer_Tick(object sender, EventArgs e)
        {
            AddAdditionalMessages(Content);

        }

    }
}
