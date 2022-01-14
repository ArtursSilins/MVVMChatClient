using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model;
using MVVMChatClient.Core.Model.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMChatClient.Core.ViewModel.Commands
{
    public class SignInRelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _sendData;
        private Action<IWindowsViewModel, IMessageContent, IJsonBaseContainer, IJsonMessageContainer> _startConnetion;
        private IMessageContent _messageContent;
        private IJsonBaseContainer _container;
        private IJsonMessageContainer _jsonMessageContainer;
        private IWindowsViewModel _windowsViewModel;

        public SignInRelayCommand(IWindowsViewModel windowsViewModel, Action sendData,
            Action<IWindowsViewModel, IMessageContent, IJsonBaseContainer, IJsonMessageContainer> startConnection,
           IMessageContent messageContent, IJsonBaseContainer container, IJsonMessageContainer jsonMessageContainer)
        {
            _sendData = sendData;
            _startConnetion = startConnection;
            _messageContent = messageContent;
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
            SignInViewModel.RaiseStartLoadingEvent();

            CurrentViewModel.SetViewModel(LogInOrSignIn.SignIn);

            if (SignInViewModel.PasswordReady)
            {
                if (!Connection.Status)
                {
                    _startConnetion.Invoke(_windowsViewModel, _messageContent, _container, _jsonMessageContainer);
                }
                else
                {
                    _sendData.Invoke();
                }
            }
            else
            {
                SignInViewModel.RaiseEndLoadingEvent(new CredentialConfirmation());
            }

        }

    }
}
