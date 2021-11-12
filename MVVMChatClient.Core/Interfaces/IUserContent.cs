using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Interfaces
{
    public interface IUserContent
    {
        string UserName { get; set; }
        string UserPicture { get; set; }
        string PersonId { get; set; }
        int FontSize { get;  }
        int NotificationText { get; set; }
        string NotificationVisibility { get; set; }


        int Size(int length);       
    }
}
