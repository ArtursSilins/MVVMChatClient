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
            IMessageContent messageContent = Factory.CreateMessageContent();

            IPerson person = Factory.CreatePerson();

            MessageList.Items = new ObservableCollection<IMessageContent>();

            IJsonBaseContainer container = Factory.CreateJsonContainer();

            IJsonMessageContainer messageContainer = Factory.CreateMessageContainer();

            IUserContent userContent = Factory.CreateUserContent();

            IUserValidationData userValidationData = new UserValidationData();

            PrivateChatViewModel privateChatViewModel = new PrivateChatViewModel(messageContainer, this);

            IChatting chatting = Factory.CreateConnectingToServer();

            ViewModels = new List<object>
            {
                new SignInViewModel(this, chatting, messageContent, person, container, messageContainer),
                new LogInViewModel(this, chatting, messageContent, person,
                container, messageContainer, userContent, userValidationData),
                new PublicChatViewModel(messageContainer, this, privateChatViewModel),
                privateChatViewModel,
                new SettingsViewModel(this),
                new PasswordSenderViewModel(this)
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

                        CurrentView = ViewModels[view];
                        break;
                    case 3:
                        if (!Connection.Status)
                            return;

                        CurrentView = ViewModels[view];
                        break;
                    case 4:
                        if (!Connection.Status)
                            return;

                        CurrentView = ViewModels[view];
                        break;
                    case 5:
                        //if (!Connection.Status)
                        //  return;
                        await Task.Delay(600);
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
