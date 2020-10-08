using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public static class PersonList
    {
        public static List<IPerson> PersonInfo { get; set; } = new List<IPerson>();
        public static IPerson GetPersonInfo()
        {
            return PersonList.PersonInfo[0];
        }
    }
}
