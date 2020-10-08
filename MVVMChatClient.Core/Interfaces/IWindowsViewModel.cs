using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Interfaces
{
    public interface IWindowsViewModel
    {
        void ChangeView(int view);


        object CurrentView { get; set; }
       
    }
}
