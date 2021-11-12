using MVVMChatClient.Core.Model;
using MVVMChatClient.Core.Model.Authorization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Interfaces
{
    public interface IJsonBaseContainer
    {
        ObservableCollection<MessageContent> Messages { get; set; }
        List<Person> Persons { get; set; }
        string CurrentPersonId { get; set; }
        Credential Credential { get; set; }
    }
}
