using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model;
using MVVMChatClient.Core.ViewModel.BaseClass;
using MVVMChatClient.Core.ViewModel.Commands;
using System.Collections.Generic;
using System.Windows.Input;

namespace MVVMChatClient.Core.ViewModel
{
    public class WindowsViewModel: ViewModelBase, IWindowsViewModel
    {
       
        private List<object> ViewModels { get; set; }

        public WindowsViewModel()
        {
            ITcpEndPoint TcpEndPoint = Factory.CreateEndPoint();

            IChatting chatting = Factory.CreateConnectingToServer();

            IMessageContent messageContent = Factory.CreateMessageContent();

            IPerson person = Factory.CreatePerson();

            MessageList.Items = new System.Collections.ObjectModel.ObservableCollection<IMessageContent>();

            IJsonContainer container = Factory.CreateJsonContainer();

            IUserContent userContent = Factory.CreateUserContent();

            ViewModels = new List<object>
            {
                new LoginViewModel(this, chatting, messageContent, person, TcpEndPoint, container, userContent),
                new ChatViewModel(messageContent, this)
            };

            currentView = ViewModels[0];

        }

        public void ChangeView(int view)
        {
            switch (view)
            {
                case 0:

                    CurrentView = ViewModels[view];
                    break;

                case 1:

                    if (!Connection.Status)
                        return;

                    CurrentView = ViewModels[view];
                    break;
            }

                                     
        }

        private object currentView;

        public object CurrentView
        {
            get { return currentView; }

            set
            {
                currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

    }
}
