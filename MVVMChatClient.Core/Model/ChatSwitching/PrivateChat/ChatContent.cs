using MVVMChatClient.Core.Interfaces;
using System.Collections.ObjectModel;

namespace MVVMChatClient.Core.Model.PrivateChat
{
    public class ChatContent
    {
        public UserContent PersonInfo { get; set; }
        public ObservableCollection<IMessageContent> PersonChat { get; set; }
    }
}