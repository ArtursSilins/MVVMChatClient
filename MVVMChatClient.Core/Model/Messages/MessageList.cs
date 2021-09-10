﻿
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
    public class MessageList:ViewModelBase
    {

        private static ObservableCollection<IMessageContent> items;

        public static ObservableCollection<IMessageContent> Items
        {
            get { return items; }
            set
            {
                items = value;
            }
        }

    }
}
