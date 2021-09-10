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
        private Action<IWindowsViewModel, ISignInViewModel, IMessageList, IMessageContent, ITcpEndPoint, IJsonContainer> _execute3;
        private SignInViewModel _userData;
        private IMessageList _messageList;
        private IMessageContent _messageContent;
        private ITcpEndPoint _tcpEndPoint;
        private IJsonContainer _container;      
        private IWindowsViewModel _windowsViewModel;
        private Action _noNameCheck;


        public ParameterRelayCommand(IWindowsViewModel windowsViewModel, Action execute2, Action<IWindowsViewModel ,ISignInViewModel, IMessageList, IMessageContent, ITcpEndPoint, IJsonContainer> execute3,
            SignInViewModel userData, IMessageList messageList, IMessageContent messageContent, ITcpEndPoint tcpEndPoint, IJsonContainer container, Action noNameCheck)
        {
            _execute2 = execute2;
            _execute3 = execute3;
            _userData = userData;
            _messageList = messageList;
            _messageContent = messageContent;
            _tcpEndPoint = tcpEndPoint;
            _container = container;;
            _windowsViewModel = windowsViewModel;
            _noNameCheck = noNameCheck;
        }
             
        public bool CanExecute(object parameter)
        {
            return true;
        }
      
        public void Execute(object parameter)
        {
            
            if(SignInViewModel.IsNameSet)
            {
                _execute2.Invoke();
                _execute3.Invoke(_windowsViewModel, _userData, _messageList, _messageContent, _tcpEndPoint, _container);
            }
            else
            {
                _noNameCheck.Invoke();
            }
                    
        }
    }
}
