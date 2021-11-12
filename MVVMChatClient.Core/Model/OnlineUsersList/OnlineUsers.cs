using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public static class OnlineUsers
    {
        public static BindingList<IUserContent> UserList { get;set; }
    }
}
