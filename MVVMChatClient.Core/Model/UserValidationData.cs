using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public class UserValidationData:IUserValidationData
    {
        public string UserName { get; set; }
        public string Pasword { get; set; }
    }
}
