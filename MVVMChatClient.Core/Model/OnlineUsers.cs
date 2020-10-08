using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public static class OnlineUsers
    {
        public static ObservableCollection<IUserContent> userList;
        public static ObservableCollection<IUserContent> UserList
        {
            get
            {
                return userList;
            }
            set
            {              
                userList = value;
            }
        }
    }
}
