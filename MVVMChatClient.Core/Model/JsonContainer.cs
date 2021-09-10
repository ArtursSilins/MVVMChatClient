using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public class JsonContainer : IJsonContainer
    {
        public ObservableCollection<MessageContent> Messages { get; set; }
        public List<Person> Persons { get; set; }
        public CurrentPersonId CurrentPersonId { get; set; }
    }
}
