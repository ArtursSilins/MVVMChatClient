using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Interfaces
{
    public interface ISignUpViewModel
    {
        string NameText { get; set; }       
        bool Male  { get; set; }                   
        bool Female { get; set; }                      

    }
}
