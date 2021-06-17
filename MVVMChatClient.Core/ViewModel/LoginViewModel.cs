using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model;
using MVVMChatClient.Core.Model.ClientComunication;
using MVVMChatClient.Core.ViewModel.BaseClass;
using MVVMChatClient.Core.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security;
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
        public ICommand LogInCommand { get; private set; }
        public ICommand SetPicFamele { get; private set; }
        public ICommand SetPicMale { get; private set; }
        public ICommand AddPic { get; private set; }
        public ICommand GetData { get; set; }
        public ICommand SwitchToSignIn { get; set; }
        public static bool FirstTimeLogin { get; set; }

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
        private SecureString password;

        public SecureString Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
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
            _person = person;
            _windowsViewModel = windowsViewModel;
            _userValidationData = userValidationData;

            //firstTime = true;

            //////////////////Vecaic variant priekš sarkanās bultas
            //IsNameSet = false;
            //ArrowVisibility = "Hidden";
            ///////////////

            //GetData = new RelayCommand(GetAppruval);//already is excecuted in LogInCommand
            SwitchToSignIn = new RelayCommand(ToSignIn);

            LogInCommand = new LogInRelayCommand(Login, _windowsViewModel, GetAppruval, chatting.Receiving2,
                this, messageContent, tcpEndPoint, container);

            

            //SetPicFamele = new RelayCommand(SetDefoultFamelePic);
            //SetPicMale = new RelayCommand(SetDefoultMalePic);
            //AddPic = new RelayCommand(AddPicture);
        }
        private void Login(object parameter)
        {
            
        }
        private void ToSignIn()
        {
            _windowsViewModel.ChangeView(0);
        }

        private void GetAppruval()
        {

            _person.Female = false;
            _person.Male = true;
            _person.Name = UserName;

            UserGender.YourGender = Gender.Male;
            UserInfo.DefaultPicture = Gender.Male;

            SwitchToSignIn = new RelayCommand(ToSignIn);

            PersonList.PersonInfo.Add(_person);

            //_userValidationData.UserName = UserName;
            ////_userValidationData.Pasword = Password;


            //if (!Client.IsConnected)
            //    Client.Connect(null);

            //if (Client.IsConnected)
            //{
            //    var messageInBytes = ConverData.ToSend(_userValidationData);

            //    Client.SendAsync(messageInBytes);

            //    Client.ReceiveAsync(TcpSocket.tcpSocket);
            //    Client.ReceiveDone.WaitOne();

            //    var apruvalMessage = ConverData.ToReceiv<UserValidationData>(Client.TextFromServer);

            //    //if( )
            //}

        }
        private void GetUserData()
        {

        }
        public void Disconnect()
        {
            //    TcpSocket.tcpSocket.Disconnect(true); 
        }
    }
}
