using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMChatClient.Core.ViewModel.Commands
{
    public class LogInRelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _execute2;
        private Action<IWindowsViewModel, IMessageContent, IJsonBaseContainer, IJsonMessageContainer> _execute3;
        private ILogInViewModel _userData;
        private IMessageContent _messageContent;
        private ITcpEndPoint _tcpEndPoint;
        private IJsonBaseContainer _container;
        private IJsonMessageContainer _jsonMessageContainer;
        private IWindowsViewModel _windowsViewModel;


        public LogInRelayCommand(IWindowsViewModel windowsViewModel, Action execute2, Action<IWindowsViewModel, IMessageContent, IJsonBaseContainer, IJsonMessageContainer> execute3,
            ILogInViewModel userData, IMessageContent messageContent, ITcpEndPoint tcpEndPoint, IJsonBaseContainer container, IJsonMessageContainer jsonMessageContainer)
        {
            _execute2 = execute2;
            _execute3 = execute3;
            _userData = userData;
            _messageContent = messageContent;
            _tcpEndPoint = tcpEndPoint;
            _container = container;
            _jsonMessageContainer = jsonMessageContainer;
            _windowsViewModel = windowsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute2.Invoke();
            _execute3.Invoke(_windowsViewModel, _messageContent, _container, _jsonMessageContainer);
        }
    }
}
