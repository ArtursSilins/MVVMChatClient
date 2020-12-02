using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Interfaces
{
    public interface IUserValidationData
    {
        string UserName { get; set; }
        string Pasword { get; set; }
    }
}
