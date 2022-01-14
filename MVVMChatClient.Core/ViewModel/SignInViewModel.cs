using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model;
using MVVMChatClient.Core.ViewModel.BaseClass;
using MVVMChatClient.Core.ViewModel.Commands;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMChatClient.Core.ViewModel
{
    public class SignInViewModel : ViewModelBase, ISignInViewModel
    {
        public static event EventHandler _sendDataEvent;
        public static event EventHandler _startLoading;
        public static event EventHandler<ICredentialConfirmation> _endLoading;
        public static event EventHandler<string> _signInControlsVisibility;

        private IWindowsViewModel _windowsViewModel;

        private IJsonBaseContainer _jsonBaseContainer;

        public ICommand SwitchToLogin { get; set; }
        public ICommand _SignIn { get; set; }

        public static bool IsNameSet { get; set; }

        public static bool PasswordReady { get; set; }
        public static bool EmptyPassword { get; set; }

        private bool AllIsCorrect { get; set; }

        private string userName;

        public string UserName
        {
            get{ return userName;}
            set
            {
                userName = value;

                if (value.Length > 0)
                    IsNameSet = true;
                else
                    IsNameSet = false;
                   
                OnPropertyChanged(nameof(UserName));
            }
        }
        private string email;
        public string Email
        {
            get { return email;}
            set
            {
                email = value;

                if (value.Length > 0)
                    IsNameSet = true;
                else
                    IsNameSet = false;

                OnPropertyChanged(nameof(Email));
            }
        }
        /// <summary>
        /// To set default value to RadioButton 
        /// </summary>
        private static bool FirstTime { get; set; }

        private bool male;

        public bool Male
        {
            get
            {
                if (FirstTime == true)
                    return male = true;
                else
                    return male;
            }
            set
            {
                FirstTime = false;

                male = value;
                OnPropertyChanged(nameof(Male));
            }
        }
        private bool female;
        public bool Female
        {
            get
            {
                return female;
            }
            set
            {
                FirstTime = false;

                female = value;
                OnPropertyChanged(nameof(Female));
            }
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        private string signInErrorVisibility;

        public string SignInErrorVisibility
        {
            get { return signInErrorVisibility; }
            set
            {
                signInErrorVisibility = value;
                OnPropertyChanged(nameof(SignInErrorVisibility));
            }
        }
        private bool signInButton;

        public bool SignInButton
        {
            get { return signInButton; }
            set
            {
                signInButton = value;
                OnPropertyChanged(nameof(SignInButton));
            }
        }

        private string nameVisbility;

        public string NameVisibility
        {
            get { return nameVisbility; }
            set
            {
                nameVisbility = value;
                OnPropertyChanged(nameof(NameVisibility));
            }
        }
        private string emailVisibility;

        public string EmailVisibility
        {
            get { return emailVisibility; }
            set
            {
                emailVisibility = value;
                OnPropertyChanged(nameof(EmailVisibility));
            }
        }
        private string maleVisibility;

        public string MaleVisibility
        {
            get { return maleVisibility; }
            set
            {
                maleVisibility = value;
                OnPropertyChanged(nameof(MaleVisibility));
            }
        }
        private string femaleVisibility;

        public string FemaleVisibility
        {
            get { return femaleVisibility; }
            set
            {
                femaleVisibility = value;
                OnPropertyChanged(nameof(FemaleVisibility));
            }
        }
        private string passwordVisibility;

        public string PasswordVisibility
        {
            get { return passwordVisibility; }
            set
            {
                passwordVisibility = value;
                OnPropertyChanged(nameof(PasswordVisibility));
            }
        }
        private string confirmPasswordVisibility;

        public string ConfirmPasswordVisibility
        {
            get { return confirmPasswordVisibility; }
            set
            {
                confirmPasswordVisibility = value;
                OnPropertyChanged(nameof(ConfirmPasswordVisibility));
            }
        }
        private string egistrationSuccess;

        public string RegistrationSuccess
        {
            get { return egistrationSuccess; }
            set
            {
                egistrationSuccess = value;
                OnPropertyChanged(nameof(RegistrationSuccess));
            }
        }
        private string signInButtonVisibility;

        public string SignInButtonVisibility
        {
            get { return signInButtonVisibility; }
            set
            {
                signInButtonVisibility = value;
                OnPropertyChanged(nameof(SignInButtonVisibility));
            }
        }
        private string checkVisibility;

        public string CheckVisibility
        {
            get { return checkVisibility; }
            set
            {
                checkVisibility = value;
                OnPropertyChanged(nameof(CheckVisibility));
            }
        }





        public SignInViewModel(IWindowsViewModel windowsViewModel,
            IChatting chatting,
            IMessageContent messageContent,
            IPerson person,
            IJsonBaseContainer container,
            IJsonMessageContainer jsonMessageContainer)
        {
            _windowsViewModel = windowsViewModel;
            _jsonBaseContainer = container;

            SignInErrorVisibility = "Hidden";

            FirstTime = true;

            IsNameSet = false;

            signInButton = true;

            EmptyPassword = true;

            _sendDataEvent += SignInViewModel__sendDataEvent;
            _endLoading += SignInViewModel__endLoading;
            _startLoading += SignInViewModel__startLoading;
            _signInControlsVisibility += SignInViewModel__signInControlsVisibility;

            SetControlsVisibility("visible");

            SwitchToLogin = new RelayCommand(ToLogin);

            _SignIn = new SignInRelayCommand(_windowsViewModel, SendData, chatting.Receiving,
                 messageContent, container, jsonMessageContainer);

        }
        public static void SetControlsVisibility(string visibility)
        {
            _signInControlsVisibility?.Invoke(typeof(SignInViewModel), visibility);
        }
        private void SignInViewModel__signInControlsVisibility(object sender, string e)
        {
            SignInControlsVisibility(e);
        }

        private void SignInViewModel__startLoading(object sender, EventArgs e)
        {
            SignInButton = false;
        }
        private bool EmailValidation(string email)
        {
            bool isValid = false;
            isValid = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            return isValid;
        }
        private void SignInViewModel__endLoading(object sender, ICredentialConfirmation credentialConfirmation)
        {
            SignInButton = true;
            if(UserName == null)
            {
                ErrorMessage = "Please enter a user name.";
                AllIsCorrect = false;
                SignInErrorVisibility = "Visible";
            }
            else if(UserName.Length < 3)
            {
                ErrorMessage = "User name is to short.";
                AllIsCorrect = false;
                SignInErrorVisibility = "Visible";
            }
             else if (Email == null)
            {
                ErrorMessage = "Please enter a valid email.";
                AllIsCorrect = false;
                SignInErrorVisibility = "Visible";
            }
            else if (!EmailValidation(Email))
            {
                ErrorMessage = "Please enter a valid email.";
                AllIsCorrect = false;
                SignInErrorVisibility = "Visible";
            }
            else if (credentialConfirmation.Name)
            {
                ErrorMessage = "User name already exists.";
                AllIsCorrect = false;
                SignInErrorVisibility = "Visible";
            }
            else if (credentialConfirmation.Email)
            {
                ErrorMessage = "This email address is already being used.";
                AllIsCorrect = false;
                SignInErrorVisibility = "Visible";
            }
            else if (EmptyPassword)
            {
                ErrorMessage = "The password field is empty.";
                SignInErrorVisibility = "Visible";
            }
            else if (!PasswordReady)
            {
                ErrorMessage = "Password mismatch.";
                SignInErrorVisibility = "Visible";
            }
            else
            {
                AllIsCorrect = true;
                signInErrorVisibility = "Hidden";
            }


        }
        public static void RaiseSendDataEvent()
        {
            _sendDataEvent?.Invoke(typeof(SignInViewModel), EventArgs.Empty);
        }
        private void SignInViewModel__sendDataEvent(object sender, EventArgs e)
        {
            SendData();
        }
        public static void RaiseStartLoadingEvent()
        {
            _startLoading?.Invoke(nameof(SignInViewModel), null);
        }
        /// <summary>
        /// Detect when data from server is received.
        /// </summary>
        public static void RaiseEndLoadingEvent(ICredentialConfirmation credentialConfirmation)
        {
            _endLoading?.Invoke(typeof(SignInViewModel), credentialConfirmation);
        }

        private void SendData()
        {
            if (PasswordReady)
            {
                _jsonBaseContainer.Credential = new Model.Authorization.Credential();
                _jsonBaseContainer.Persons = new System.Collections.Generic.List<Person>();

                _jsonBaseContainer.Credential.UserName = UserName;
                _jsonBaseContainer.Credential.Email = Email;
                _jsonBaseContainer.Credential.Sex = Gender.Check(Male);
                _jsonBaseContainer.Credential.Password = new NetworkCredential("", Model.Security.UserPassword.Password).Password;
                _jsonBaseContainer.Credential.NeedAction = true;
                _jsonBaseContainer.Credential.NeedKeys = false;
                _jsonBaseContainer.Credential.Login = false;
                _jsonBaseContainer.Credential.SignIn = true;

                var messageInBytes = ConverData.ToSend(_jsonBaseContainer);

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

                _jsonBaseContainer.Credential = null;
            }
            else
            {

                SignInErrorVisibility = "Visible";
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
        /// <summary>
        /// Determine whether to show controls or success registration message.
        /// </summary>
        /// <param name="value"></param>
        private void SignInControlsVisibility(string value)
        {
            NameVisibility = value;
            EmailVisibility = value;
            MaleVisibility = value;
            FemaleVisibility = value;
            PasswordVisibility = value;
            ConfirmPasswordVisibility = value;
            SignInButtonVisibility = value;

            if (value == "visible") { RegistrationSuccess = "hidden"; CheckVisibility = "hidden"; }

            if (value == "hidden") { RegistrationSuccess = "visible"; CheckVisibility = "visible"; }
        }

        private void ToLogin()
        {
            _windowsViewModel.ChangeView(1);
            SignInControlsVisibility("visible");
        }

        public void Disconnect()
        {
        //    TcpSocket.tcpSocket.Disconnect(true); 
        }
    }
}
