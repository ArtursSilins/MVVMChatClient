using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model.OnlineUsersList
{
    /// <summary>
    /// Message received notification.
    /// </summary>
    public static class Notifications
    {
        public static string Id { get; set; }
        private static int IndexCounter { get; set; } = 0;
        /// <summary>
        /// Add message received notification count.
        /// </summary>
        public static void Add()
        {
            foreach (var item in OnlineUsers.UserList)
            {
                if (item.PersonId == Id)
                {
                    item.NotificationText++;
                    item.NotificationVisibility = "Visible";

                    OnlineUsers.UserList.ResetItem(IndexCounter);
                }
                IndexCounter++;
            }

            IndexCounter = 0;
        }
        /// <summary>
        /// Remove message received notification count.
        /// </summary>
        public static void Remove()
        {
            foreach (var item in OnlineUsers.UserList)
            {
                if (item.PersonId == Id)
                {
                    item.NotificationText = 0;
                    item.NotificationVisibility = "Hidden";
                }
            }
        }
    }
}
