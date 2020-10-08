using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Interfaces
{
    public interface IClientData
    {
        bool Female { get; set; }
        bool Male { get; set; }
        int PersonId { get; set; }
        string UserName { get; set; }
    }
}
