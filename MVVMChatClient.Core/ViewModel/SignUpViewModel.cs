using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model;
using MVVMChatClient.Core.ViewModel.BaseClass;
using MVVMChatClient.Core.ViewModel.Commands;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMChatClient.Core.ViewModel
{
    public class SignUpViewModel : ViewModelBase, ISignUpViewModel
    {

        private IWindowsViewModel _windowsViewModel;
        private IPerson _person;
        public ICommand SetView { get; private set; }
        public ICommand SetPicFamele { get; private set; }
        public ICommand SetPicMale { get; private set; }
        public ICommand AddPic { get; private set; }


        public bool firstTime { get; set; }
        private bool IsPicture { get; set; }
        public static bool IsNameSet { get; set; }

        private string nameText;

        public string NameText
        {
            get
            {
                            
                return nameText;
            }
            set
            {
                nameText = value;

                if (value.Length > 0)
                    IsNameSet = true;
                else
                    IsNameSet = false;

                   
                OnPropertyChanged(nameof(NameText));

            }
        }
        private bool male;

        private bool noText;

        public bool NoText
        {
            get
            {
                return noText;
            }
            set
            {
                noText = value;
                OnPropertyChanged(nameof(NoText));
            }
        }

        private string arrowVisibility;

        public string ArrowVisibility
        {
            get
            {
                return arrowVisibility;
            }
            set
            {
                arrowVisibility = value;
                OnPropertyChanged(nameof(ArrowVisibility));
            }
        }


        public bool Male
        {
            get
            {
                if (firstTime == true)
                    return male = true;
                else
                    return male;
            }
            set
            {
                firstTime = false;

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
                firstTime = false;

                female = value;
                OnPropertyChanged(nameof(Female));

                if (!IsPicture)
                    profilePicture = Gender.Female;
            }
        }

        private string profilePicture;
        public string ProfilePicture
        {
            get
            {
                if (profilePicture == null)
                    profilePicture = Gender.Male;
              
                return profilePicture;
            }
            set
            {                
                profilePicture = value;
                if (profilePicture != "")
                    OnPropertyChanged(nameof(ProfilePicture));
                else
                    IsPicture = false;
                   
            }
        }
        
        public SignUpViewModel(IWindowsViewModel windowsViewModel,
            IChatting chatting,
            IMessageContent messageContent,
            IPerson person,
            ITcpEndPoint tcpEndPoint,
            IJsonContainer container,
            IUserContent userContent)
        {
            _person = person;
            _windowsViewModel = windowsViewModel;

            firstTime = true;
            IsNameSet = false;
            ArrowVisibility = "Hidden";

            SetView = new ParameterRelayCommand(_windowsViewModel, GetUserData, chatting.Receiving,
                this, messageContent, tcpEndPoint, container, NoNameCheck);

            SetPicFamele = new RelayCommand(SetDefoultFamelePic);
            SetPicMale = new RelayCommand(SetDefoultMalePic);
            AddPic = new RelayCommand(AddPicture);

        }

        public void SetDefoultFamelePic()
        {
            if (!IsPicture)
                ProfilePicture = Gender.Female;
        }
        public void SetDefoultMalePic()
        {
            if (!IsPicture)
                ProfilePicture = Gender.Male;
        }
        private void AddPicture()
        {
            IsPicture = true;

            ProfilePicture = FilePath.Get();
            UserInfo.AddedPicture = ProfilePicture;
        }

        private void NoNameCheck()
        {
            if (_person.Name == "" || _person.Name == null)
            {
                ArrowVisibility = "Visible";
                NoText = true;
                NoText = false;
            }              
            else
                NoText = false;
        }
        private void GetUserData()
        {
          
            _person.Name = NameText;
            _person.Male = Male;
            _person.Female = Female;

            if (UserInfo.AddedPicture != null)
            {
                _person.Pic = ConvertImage.imageConverter.ImageToByte(UserInfo.AddedPicture);
            }

            UserInfo.Name = NameText;

            if (Male)
            {
                UserGender.YourGender = Gender.Male;
                UserInfo.DefaultPicture = Gender.Male;
                
            }
            if (Female)
            {
                UserGender.YourGender = Gender.Female;
                UserInfo.DefaultPicture = Gender.Female;
            }


            PersonList.PersonInfo.Add(_person);
            
        }      
        public void Disconnect()
        {
        //    TcpSocket.tcpSocket.Disconnect(true); 
        }
    }
}
