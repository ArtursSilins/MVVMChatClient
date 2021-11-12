using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model.KeyContainer
{
    public static class SymmetricKey
    {
        public static RijndaelManaged UserKeys { get; set; }

    }
}
