using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVMChatClient.Core.Interfaces;

namespace MVVMChatClient.Core.ViewModel.Commands
{
    public class ParameterRelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _execute2;
        private Action<IWindowsViewModel, ILoginViewModel, IMessageContent, ITcpEndPoint, IJsonContainer> _execute3;
        private LoginViewModel _userData;
        private IMessageContent _messageContent;
        private ITcpEndPoint _tcpEndPoint;
        private IJsonContainer _container;      
        private IWindowsViewModel _windowsViewModel;


        public ParameterRelayCommand(IWindowsViewModel windowsViewModel, Action execute2, Action<IWindowsViewModel ,ILoginViewModel, IMessageContent, ITcpEndPoint, IJsonContainer> execute3,
            LoginViewModel userData, IMessageContent messageContent, ITcpEndPoint tcpEndPoint, IJsonContainer container)
        {
            _execute2 = execute2;
            _execute3 = execute3;
            _userData = userData;
            _messageContent = messageContent;
            _tcpEndPoint = tcpEndPoint;
            _container = container;;
            _windowsViewModel = windowsViewModel;
        }
             
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(LoginViewModel.IsNameSet)
            {
                _execute2.Invoke();
                _execute3.Invoke(_windowsViewModel, _userData, _messageContent, _tcpEndPoint, _container);
            }
            
        }
    }
}
