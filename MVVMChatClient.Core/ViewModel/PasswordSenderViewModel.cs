using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.ViewModel.BaseClass;
using MVVMChatClient.Core.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMChatClient.Core.ViewModel
{
    public class PasswordSenderViewModel : ViewModelBase
    {
        private IWindowsViewModel _windowsViewModel;
        public ICommand _GoBack { get; set; }
        private ICommand _Send { get; set; }
        public PasswordSenderViewModel(IWindowsViewModel windowsViewModel)
        {
            _windowsViewModel = windowsViewModel;

            _GoBack = new RelayCommand(GoBack);
            _Send = new RelayCommand(Send);
        }

        private void Send()
        {
            throw new NotImplementedException();
        }

        private void GoBack()
        {
            _windowsViewModel.ChangeView(1);
        }
        public void Disconnect()
        {
            //    TcpSocket.tcpSocket.Disconnect(true); 
        }
    }
}
