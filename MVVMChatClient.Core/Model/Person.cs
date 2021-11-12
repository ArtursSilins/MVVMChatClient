using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public class Person : IPerson
    {
        public string Name { get; set; }
        public bool Male { get; set; }
        public bool Female { get; set; }
        public Socket Connection { get; set; }
        public string PersonId { get; set; }
        public string PicturePath { get; set; }
        public byte[] Pic { get; set; }

    }
}
