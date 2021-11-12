using MVVMChatClient.Core.Model;
using MVVMChatClient.Core.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMChatClient.Core.ViewModel.BaseClass
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
      
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => {};
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
            
        }

        public static event PropertyChangedEventHandler PropertyChangedStatic = (s, e) => { };
        public void OnPropertyChangedStatic(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));

        }

    }
  
}
