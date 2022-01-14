using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model.ChatSwitching;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MVVMChatClient.Core.Model.PrivateChat
{
    public class ChatListHolder:MessageAddControl
    {
        public static Dictionary<string, ObservableCollection<IMessageContent>> Content { get; set; } =
            new Dictionary<string, ObservableCollection<IMessageContent>>();

        private static string UserID;

        public static void GetPersonChatFirstTime(string userID)
        {
            UserID = userID;

            if (Content.ContainsKey(UserID))
            {
                AddTimer = new DispatcherTimer();
                AddTimer = new DispatcherTimer(DispatcherPriority.SystemIdle);
                AddTimer.Interval = TimeSpan.FromMilliseconds(5);
                AddTimer.Tick += AddTimer_Tick;
                AddTimer.Start();
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
        }

        private static void AddTimer_Tick(object sender, EventArgs e)
        {
            AddFirstTimeMessages(Content[UserID]);
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

            AddAdditionalMessages(Content[UserID]);

        }


        public static void AddToPersonChat(List<string> userID, IMessageContent messageContent)
        {
            foreach (var item in userID)
            {
                if (item != User.Id)
                {
                    OnlineUsersList.Notifications.Id = item;

                    if (Content.ContainsKey(item))
                        Content[item].Add(messageContent);
                    else
                        Content.Add(item,
                        new ObservableCollection<IMessageContent>() { messageContent.NewInstance(messageContent) });
                }
            }

        }

        public static void RemovePrivateChat()
        {

        }
    }
}
