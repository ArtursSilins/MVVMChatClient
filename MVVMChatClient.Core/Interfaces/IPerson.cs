using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Interfaces
{
    public interface IPerson
    {
        string Name { get; set; }
        bool Male { get; set; }
        bool Female { get; set; }
        Socket Connection { get; set; }
        string PersonId { get; set; }
        string PicturePath { get; set; }
        byte[] Pic { get; set; }
    }
}
