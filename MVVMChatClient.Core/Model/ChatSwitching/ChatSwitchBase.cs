using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model.ChatSwitching
{
    public class ChatSwitchBase
    {
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
        }
    }
}
