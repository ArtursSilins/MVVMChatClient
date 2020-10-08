using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Interfaces
{
    public interface ILoginViewModel
    {
        string NameText { get; set; }       
        bool Male  { get; set; }                   
        bool Female { get; set; }                      

    }
}
