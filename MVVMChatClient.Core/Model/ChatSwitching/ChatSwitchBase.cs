using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MVVMChatClient.Core.Model.ChatSwitching
{
    public class ChatSwitchBase
    {
        public static DispatcherTimer WaitTimer { get; set; }

        public static DispatcherTimer AddTimer { get; set; }

        public static DispatcherTimer AddAdditionalTimer { get; set; }
        public static bool AllItemsAdded { get; set; }
        public static int AddCount { get; set; }
        public static int AddRangeCount { get; set; }
        public static bool FinishAdd { get; set; } = true;

        public static void RessetData()
        {
            AllItemsAdded = false;
            AddCount = 0;
            AddRangeCount = 0;
            FinishAdd = true;
            WaitTimer?.Stop();
            AddTimer?.Stop();
            AddAdditionalTimer?.Stop();
        }

        protected static void AddFirstTimeMessages(ObservableCollection<IMessageContent> content)
        {
            if (AddCount <= content.Count - 1 &&
                            MessageList.Items.Count < 30 &&
                            MessageList.Items.Count < content.Count &&
                            content.Count != 0)
            {
                if (content.Count >= 30)
                    MessageList.Items.Add(content[AddCount + content.Count - 30]);
                else
                    MessageList.Items.Add(content[AddCount]);

                AddCount++;

            }
            else
            {
                AddCount = content.Count - 31;
                AddTimer.Stop();
            }
        }
        protected static void AddAdditionalMessages(ObservableCollection<IMessageContent> content)
        {
            if (AddRangeCount < 10 && 0 <= AddCount)
            {
                MessageList.Items.Insert(0, content[AddCount]);
                AddCount--;
                AddRangeCount++;
            }
            else if (AddCount < 0)
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
    }
}
