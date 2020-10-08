using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Interfaces
{
    public interface IDisconnectContent
    {
        string ExitMessage { get; set; }
        int Id { get; set; } 
    }
}
