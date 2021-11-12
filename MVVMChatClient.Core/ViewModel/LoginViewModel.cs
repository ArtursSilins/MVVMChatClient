using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model;
using MVVMChatClient.Core.Model.ClientComunication;
using MVVMChatClient.Core.Model.EncryptAndDecrypt;
using MVVMChatClient.Core.ViewModel.BaseClass;
using MVVMChatClient.Core.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Security.Cryptography;
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

        private IJsonMessageContainer _jsonMessageContainer;

        private ICredential _credential;

        private IJsonBaseContainer _container;

        public ICommand LogInCommand { get; private set; }
        public ICommand SetPicFamele { get; private set; }
        public ICommand SetPicMale { get; private set; }
        public ICommand AddPic { get; private set; }
        public ICommand GetData { get; set; }
        public ICommand SwitchToSignIn { get; set; }
        public static bool FirstTimeLogin { get; set; }
        public static bool PasswordReady { get; set; }
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
        public LogInViewModel(IWindowsViewModel windowsViewModel,
            IChatting chatting,
            IMessageContent messageContent,
            IPerson person,
            ITcpEndPoint tcpEndPoint,
            IJsonBaseContainer container,
            IJsonMessageContainer jsonMessageContainer,
            IUserContent userContent,
            IUserValidationData userValidationData,
            ICredential credential)
        {
            _container = container;
            _person = person;
            _windowsViewModel = windowsViewModel;
            _userValidationData = userValidationData;
            _jsonMessageContainer = jsonMessageContainer;
            _credential = credential;
            //test enconding
            //MessageTextEncryption.RSACreate();

            //šitas strādā
            //string textEncrypted = MessageTextEncryption.Encrypt("Jipers Krīpers!");
            //string decrypt = MessageTextEncryption.Decrypt(textEncrypted);

            AsymmetricEncryption.CreateKey("Test");
            string pubKey = AsymmetricEncryption.PubKeyString("Test");

            RSAParameters publicKey = AsymmetricEncryption.StringToPubKey(pubKey);

            string testEn = MessageTextEncryption.TestEncrypt("abc", /*publicKey*/StoreAsymmetricKeys.GetPublicKeyFromContainer("Test"));
            string testDe = MessageTextEncryption.TestDecrypt(testEn/*, StoreAsymmetricKeys.GetPrivateKeyFromContainer("Test")*/);
            // 
            //  1. COMPARE RECEIVE KEY WITH ACTUAL KEY
            //  2. EDIT KEY CONTAINER IN ŠITAS STRĀDĀ
            

            //firstTime = true;

            //////////////////Vecaic variant priekš sarkanās bultas
            //IsNameSet = false;
            //ArrowVisibility = "Hidden";
            ///////////////

            //GetData = new RelayCommand(GetAppruval);//already is excecuted in LogInCommand
            SwitchToSignIn = new RelayCommand(ToSignIn);

            //LogInCommand = new LogInRelayCommand(_windowsViewModel, GetAppruval, chatting.Receiving,
            //    this, messageContent, tcpEndPoint, container, jsonMessageContainer, credential);
            LogInCommand = new RelayCommand(SendData);
            

            //SetPicFamele = new RelayCommand(SetDefoultFamelePic);
            //SetPicMale = new RelayCommand(SetDefoultMalePic);
            //AddPic = new RelayCommand(AddPicture);
        }

        private void SendData()
        {
            if (PasswordReady)
            {
                _container.Credential = new Model.Authorization.Credential();

                _container.Credential.UserName = UserName;
                _container.Credential.Password = new NetworkCredential("", Model.Security.UserPassword.Password).Password;
                _container.Credential.NeedAction = true;
                _container.Credential.NeedKeys = false;
                _container.Credential.Login = true;
                _container.Credential.SignIn = false;

                var messageInBytes = ConverData.ToSend(_container);

                try
                {
                    TcpSocket.tcpSocket.BeginSend(messageInBytes, 0, messageInBytes.Length, SocketFlags.None, new AsyncCallback(SendCallback), TcpSocket.tcpSocket);
                }
                catch (Exception ex)
                {
                    AlertMessages.Show(ex.Message);

                    OnlineUsers.UserList.Clear();

                    TcpSocket.tcpSocket.Shutdown(SocketShutdown.Both);
                    TcpSocket.tcpSocket.Disconnect(true);

                    _windowsViewModel.ChangeView(0);

                    return;
                }

                _container.Credential = null;
            }
        }
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;

                int bytesSent = client.EndSend(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
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

            UserInfo.Name = UserName;

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
