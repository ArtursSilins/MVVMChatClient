using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public enum ChatMode
    {
        Public = 0,
        Private = 1
    }
    public class ChatSwitch
    {
        public ChatMode ChatMode { get; set; }

    }
}
