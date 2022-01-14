using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MVVMChatClient.Core.Model.ChatSwitching
{
    public class MessageAddControl
    {
        public static DispatcherTimer WaitTimer { get; set; }

        public static DispatcherTimer AddTimer { get; set; }

        public static DispatcherTimer AddAdditionalTimer { get; set; }
        public static bool AllItemsAdded { get; set; }
        public static int AddCount { get; set; }
        public static int AddRangeCount { get; set; }
        public static bool FinishAdd { get; set; } = true;

        private static int TotalPublicMessages { get; set; }

        /// <summary>
        /// Reset all add contorl data to the default value.
        /// </summary>
        public static void ResetData()
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
                            MessageList.Items.Count < 20 &&
                            MessageList.Items.Count < content.Count &&
                            content.Count != 0)
            {
                if (content.Count >= 20)
                    MessageList.Items.Add(content[AddCount + content.Count - 20]);
                else
                    MessageList.Items.Add(content[AddCount]);

                AddCount++;

            }
            else
            {
                AddCount = content.Count - 21;
                AddTimer.Stop();
            }
        }
        protected static void AddFirstTimeMessages(ObservableCollection<IMessageContent> content,
            SynchronizationContext uiContext)
        {
            if (AddCount <= content.Count - 1 &&
                            MessageList.Items.Count < 20 &&
                            MessageList.Items.Count < content.Count &&
                            content.Count != 0)
            {
                if (content.Count >= 20)
                    uiContext.Send(x => MessageList.Items.Add(content[AddCount + content.Count - 20]), null);
                else
                    uiContext.Send(x => MessageList.Items.Add(content[AddCount]), null);

                AddCount++;

            }
            else
            {
                AddCount = content.Count - 21;
                TotalPublicMessages = content.Count;
            }
        }
        protected static void AddAdditionalMessages(ObservableCollection<IMessageContent> content)
        {
            if (AddRangeCount < 20 && 0 <= AddCount)
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
                AddRangeCount = 0;
                AddAdditionalTimer.Stop();
                FinishAdd = true;
            }
        }
        public static int GetMessageCount()
        {
            return TotalPublicMessages - AddCount;
        }
    }
}
