using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Interfaces
{
    public interface ICredential
    {
        bool Login { get; set; }
        bool SignIn { get; set; }
        bool NeedKeys { get; set; }
        bool NeedAction { get; set; }
        bool PasswordConfirmed { get; set; }
        string PubKey { get; set; }
        string SymmetricKey { get; set; }
        string IV { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        int Sex { get; set; }
    }
}
