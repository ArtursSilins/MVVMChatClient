using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model;
using MVVMChatClient.Core.Model.ClientComunication;
using MVVMChatClient.Core.ViewModel.BaseClass;
using MVVMChatClient.Core.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMChatClient.Core.ViewModel
{
    public class LogInViewModel : ViewModelBase, ILogInViewModel
    {
        private IWindowsViewModel _windowsViewModel;
        private IPerson _person;
        private IUserValidationData _userValidationData;
        public ICommand SetView { get; private set; }
        public ICommand SetPicFamele { get; private set; }
        public ICommand SetPicMale { get; private set; }
        public ICommand AddPic { get; private set; }
        public ICommand GetData { get; set; }
        private ICommand Login { get; set; }

        private string userName;

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        private string pasword;

        public string Pasword
        {
            get
            {
                return pasword;
            }
            set
            {
                pasword = value;
                OnPropertyChanged(nameof(Pasword));
            }
        }
        public LogInViewModel(IWindowsViewModel windowsViewModel,
            IChatting chatting,
            IMessageContent messageContent,
            IPerson person,
            ITcpEndPoint tcpEndPoint,
            IJsonContainer container,
            IUserContent userContent,
            IUserValidationData userValidationData)
        {
            //_person = person;
            _windowsViewModel = windowsViewModel;
            _userValidationData = userValidationData;

            //firstTime = true;
            //IsNameSet = false;
            //ArrowVisibility = "Hidden";

            GetData = new RelayCommand(GetAppruval);

            //SetView = new LogInRelayCommand(_windowsViewModel, GetUserData, chatting.Receiving,
            //    this, messageContent, tcpEndPoint, container);

            //SetPicFamele = new RelayCommand(SetDefoultFamelePic);
            //SetPicMale = new RelayCommand(SetDefoultMalePic);
            //AddPic = new RelayCommand(AddPicture);
        }
        private void GetAppruval()
        {
            _userValidationData.UserName = UserName;
            _userValidationData.Pasword = Pasword;


            if (!Client.IsConnected)
                Client.Connect(null);

            if (Client.IsConnected)
            {
                var messageInBytes = ConverData.ToSend(_userValidationData);

                Client.SendAsync(messageInBytes);

                Client.ReceiveAsync(TcpSocket.tcpSocket);
                Client.ReceiveDone.WaitOne();

                var apruvalMessage = ConverData.ToReceiv<UserValidationData>(Client.TextFromServer);

                //if( )
            }

        }
        private void GetUserData()
        {

        }
    }
}
