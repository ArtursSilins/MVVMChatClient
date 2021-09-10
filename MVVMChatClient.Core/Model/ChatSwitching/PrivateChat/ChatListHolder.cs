using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MVVMChatClient.Core.Model.PrivateChat
{
    public static class ChatListHolder
    {
        public static Dictionary<int, ObservableCollection<IMessageContent>> Content { get; set; } =
            new Dictionary<int, ObservableCollection<IMessageContent>>();

        private static DispatcherTimer Timer;
        private static int UserID;


        public static void GetPersonChat(int userID)
        {
            UserID = userID;

            Timer = new DispatcherTimer();
            Timer = new DispatcherTimer(DispatcherPriority.SystemIdle);
            Timer.Interval = TimeSpan.FromMilliseconds(800);
            Timer.Tick += Timer_Tick;
            Timer.Start();

        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            if (Content.ContainsKey(UserID))
            {
                foreach (var item in Content[UserID])
                {
                    MessageList.Items.Add(item);
                }
            }
            else
            {
                Content.Add(UserID,
                    new ObservableCollection<IMessageContent>());

                foreach (var item in Content[UserID])
                {
                    MessageList.Items.Add(item);
                }
            }

            Timer.Stop();
        }

        public static void AddToPersonChat(int userID, IMessageContent messageContent)
        {
            if (Content.ContainsKey(userID))
                Content[userID].Add(messageContent);
            else
                Content.Add(userID,
                new ObservableCollection<IMessageContent>() { messageContent.NewInstance(messageContent) });
        }
        public static void RemovePrivateChat()
        {

        }
    }
}
