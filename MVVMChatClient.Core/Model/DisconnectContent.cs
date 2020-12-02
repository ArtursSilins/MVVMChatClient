using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public class DisconnectContent : IDisconnectContent
    {
        public string ExitMessage { get; set; } = "€noc§dne§€";
        public int Id { get; set; } = User.Id;
    }
}
