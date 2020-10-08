using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public class ClientData : IClientData
    {
        public string UserName { get; set; }
        public bool Male { get; set; }
        public bool Female { get; set; }
        public int PersonId { get; set; }
    }
}
