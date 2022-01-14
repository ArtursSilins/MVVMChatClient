using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model;
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
using System.Windows.Threading;

namespace MVVMChatClient.Core.ViewModel
{
    public class LogInViewModel : ViewModelBase, ILogInViewModel
    {
        public static event EventHandler _startLoading;
        public static event EventHandler<bool> _endLoading;

        public static event EventHandler _sendDataEvent;

        private IWindowsViewModel _windowsViewModel;
        private IPerson _person;
        private IUserValidationData _userValidationData;

        private IJsonMessageContainer _jsonMessageContainer;

        private IJsonBaseContainer _container;

        public ICommand LogInCommand { get; private set; }
        public ICommand SetPicFamele { get; private set; }
        public ICommand SetPicMale { get; private set; }
        public ICommand AddPic { get; private set; }
        public ICommand GetData { get; set; }
        public ICommand SwitchToSignIn { get; set; }
        public ICommand ForgotPassword { get; set; }
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

        private string logInErrorMessage;

        public string LogInErrorMessage
        {
            get { return logInErrorMessage; }
            set
            {
                logInErrorMessage = value;
                OnPropertyChanged(nameof(LogInErrorMessage));
            }
        }


        /// <summary>
        /// Visibility of Button
        /// </summary>
        private bool logInButton;

        public bool LogInButton
        {
            get { return logInButton; }
            set
            {
                logInButton = value;
                OnPropertyChanged(nameof(LogInButton));
            }
        }

        public LogInViewModel(IWindowsViewModel windowsViewModel,
            IChatting chatting,
            IMessageContent messageContent,
            IPerson person,
            IJsonBaseContainer container,
            IJsonMessageContainer jsonMessageContainer,
            IUserContent userContent,
            IUserValidationData userValidationData)
        {
            _container = container;
            _person = person;
            _windowsViewModel = windowsViewModel;
            _userValidationData = userValidationData;
            _jsonMessageContainer = jsonMessageContainer;

            LogInButton = true;
            LogInErrorMessage = "Hidden";

            SwitchToSignIn = new RelayCommand(ToSignIn);

            LogInCommand = new LogInRelayCommand(_windowsViewModel, SendData, chatting.Receiving,
                messageContent, container, jsonMessageContainer);

            ForgotPassword = new RelayCommand(GoToPasswordSender);

            _sendDataEvent += LogInViewModel__sendDataEvent;
            _startLoading += LogInViewModel__startLoading;
            _endLoading += LogInViewModel__endLoading1;

            //SetPicFamele = new RelayCommand(SetDefoultFamelePic);
            //SetPicMale = new RelayCommand(SetDefoultMalePic);
            //AddPic = new RelayCommand(AddPicture);
        }

        private void GoToPasswordSender()
        {
            _windowsViewModel.ChangeView(5);
        }

        private void LogInViewModel__endLoading1(object sender, bool e)
        {
            LogInButton = true;

            if (!e)
                LogInErrorMessage = "Visible";
            else
                LogInErrorMessage = "Hidden";
           
        }

        //private void LogInViewModel__endLoading(object sender, EventArgs e)
        //{
        //    LogInButton = true;
        //}

        private void LogInViewModel__startLoading(object sender, EventArgs e)
        {
            LogInButton = false;
        }
        /// <summary>
        /// Detect when LogIn button is pressed.
        /// </summary>
        public static void RaiseStartLoadingEvent()
        {
            _startLoading?.Invoke(typeof(LogInViewModel), EventArgs.Empty);
        }
        /// <summary>
        /// Detect when data from server is received.
        /// </summary>
        public static void RaiseEndLoadingEvent(bool exists)
        {
            _endLoading?.Invoke(typeof(LogInViewModel), exists);
        }
        /// <summary>
        /// Send data to server after Keys are received.
        /// </summary>
        public static void RaiseSendDataEvent()
        {
            _sendDataEvent?.Invoke(typeof(LogInViewModel), EventArgs.Empty);
        }
        private void LogInViewModel__sendDataEvent(object sender, EventArgs e)
        {
            SendData();
        }

        private void SendData()
        {
            if (!PasswordReady)////////////////////TEST !!!!
            {


                _container.Credential = new Model.Authorization.Credential();

                _container.Credential.UserName = "Artūrs";//UserName;
                _container.Credential.Password = "12345";//new NetworkCredential("", Model.Security.UserPassword.Password).Password;
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

        public void Disconnect()
        {
            //    TcpSocket.tcpSocket.Disconnect(true); 
        }
    }
}
