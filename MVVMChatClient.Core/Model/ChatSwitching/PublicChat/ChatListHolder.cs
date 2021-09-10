using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model.ChatSwitching;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MVVMChatClient.Core.Model.PublicChat
{
    public class ChatListHolder:ChatSwitchBase
    {
        public static ObservableCollection<IMessageContent> Content { get; set; } = 
            new ObservableCollection<IMessageContent>();

        private static DispatcherTimer WaitTimer;

        private static DispatcherTimer AddTimer;

        private static DispatcherTimer AddAdditionalTimer;


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

            if (AddCount <= Content.Count - 1 && MessageList.Items.Count < 30)
            {
                MessageList.Items.Add(Content[AddCount + Content.Count - 30]);
                AddCount++;

            }
            else
            {
                AddCount = Content.Count - 31;
                AddTimer.Stop(); 
            }
        }

        public static void AddAdditionalMessages()
        {
            AddAdditionalTimer = new DispatcherTimer();
            AddAdditionalTimer = new DispatcherTimer(DispatcherPriority.SystemIdle);
            AddAdditionalTimer.Interval = TimeSpan.FromMilliseconds(5);
            AddAdditionalTimer.Tick += AddAdditionalTimer_Tick;
            AddAdditionalTimer.Start();
        }

        private static void AddAdditionalTimer_Tick(object sender, EventArgs e)
        {

            if (AddRangeCount<10 && 0 <= AddCount)
            {
                MessageList.Items.Insert(0, Content[AddCount]);
                AddCount--;
                AddRangeCount++;
            }
            else if(AddCount<0)
            {
                AllItemsAdded = true;
                AddAdditionalTimer.Stop();

            }
            else
            {
                FinishAdd = true;
                AddRangeCount = 0;
                AddAdditionalTimer.Stop();
            }

        }

        public static void RessetMessageCount() => AddCount = 0;

        public static void TestPopulateMessages()
        {
            int count = 0;

            while (count<=100)
            {
                MessageContent messageContent = new MessageContent()
                {
                    Id = 1, MessageAlignment = "Right", MessageColour = "White",
                    MessagePictureVisibility = "Hidden", MessageText = count.ToString(),
                    MessageTime = "00:00", Name = "Archi", MessagePicture = Gender.Male
                };

                Content.Add(messageContent);

                count++;
            }

            
        }
    }
}
