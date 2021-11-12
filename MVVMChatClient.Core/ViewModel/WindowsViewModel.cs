using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model;
using MVVMChatClient.Core.ViewModel.BaseClass;
using MVVMChatClient.Core.ViewModel.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMChatClient.Core.ViewModel
{
    public class WindowsViewModel: ViewModelBase, IWindowsViewModel
    {
       
        private List<object> ViewModels { get; set; }

        public WindowsViewModel()
        {
            ITcpEndPoint TcpEndPoint = Factory.CreateEndPoint();

            IMessageContent messageContent = Factory.CreateMessageContent();

            IPerson person = Factory.CreatePerson();

            MessageList.Items = new ObservableCollection<IMessageContent>();

            IJsonBaseContainer container = Factory.CreateJsonContainer();

            IJsonMessageContainer messageContainer = Factory.CreateMessageContainer();

            IUserContent userContent = Factory.CreateUserContent();

            IUserValidationData userValidationData = new UserValidationData();

            PrivateChatViewModel privateChatViewModel = new PrivateChatViewModel(messageContainer, this);

            ICredential credential = Factory.CreateCredential();

            IChatting chatting = Factory.CreateConnectingToServer();
            chatting.Receiving(this, messageContent, container, messageContainer, credential);


            ViewModels = new List<object>
            {
                new SignInViewModel(this, chatting, messageContent, person, TcpEndPoint, container, userContent),
                new LogInViewModel(this, chatting, messageContent, person, TcpEndPoint,
                container, messageContainer, userContent, userValidationData, credential),
                new PublicChatViewModel(messageContainer, this, privateChatViewModel),
                privateChatViewModel
            };

            currentView = ViewModels[1];

        }

        public void ChangeView(int view)
        {
            var t = Task.Run(async delegate
            {
                switch (view)
                {
                    case 0:
                        await Task.Delay(600);
                        CurrentView = ViewModels[view];
                        break;

                    case 1:
                        await Task.Delay(600);
                        CurrentView = ViewModels[view];
                        break;

                    case 2:
                        if (!Connection.Status)
                            return;

                        //await Task.Delay(600);
                        CurrentView = ViewModels[view];
                        break;
                    case 3:
                        if (!Connection.Status)
                            return;

                        //await Task.Delay(600);
                        CurrentView = ViewModels[view];
                        break;
                }

            });
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
