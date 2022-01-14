using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Interfaces
{
    public interface ICredentialConfirmation
    {
        bool Status { get; set; }
        bool Name { get; set; }
        bool Email { get; set; }
        bool Login { get; set; }
        bool SignIn { get; set; }
    }
}
