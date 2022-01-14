using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model;
using MVVMChatClient.Core.ViewModel.BaseClass;
using MVVMChatClient.Core.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMChatClient.Core.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private IDisconnectContent _disconnectContent;
        public ICommand _Exit { get; private set; }

        private IWindowsViewModel _windowsViewModel;
        public ICommand SetPicFamele { get; private set; }
        public ICommand SetPicMale { get; private set; }
        public ICommand AddPic { get; private set; }
        public bool IsPicture { get; private set; }

        public bool firstTime { get; set; }

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

        private bool male;

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


        public SettingsViewModel(IWindowsViewModel windowsViewModel)
        {
            _windowsViewModel = windowsViewModel;

            SetPicFamele = new RelayCommand(SetDefoultFamelePic);
            SetPicMale = new RelayCommand(SetDefoultMalePic);
            AddPic = new RelayCommand(AddPicture);

            _Exit = new RelayCommand(Exit);
        }

        private void Exit()
        {
            _windowsViewModel.ChangeView(2);
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

        //private void NoNameCheck()
        //{
        //    if (_person.Name == "" || _person.Name == null)
        //    {
        //        ArrowVisibility = "Visible";
        //        NoText = true;
        //        NoText = false;
        //    }
        //    else
        //        NoText = false;
        //}
        //private void GetUserData()
        //{

        //    _person.Name = NameText;

        //    if (Female) _person.Sex = 2;
        //    if (Male) _person.Sex = 1;

        //    if (UserInfo.AddedPicture != null)
        //    {
        //        _person.Pic = ConvertImage.imageConverter.ImageToByte(UserInfo.AddedPicture);
        //    }

        //    UserInfo.Name = NameText;

        //    if (Male)
        //    {
        //        UserGender.YourGender = Gender.Male;
        //        UserInfo.DefaultPicture = Gender.Male;

        //    }
        //    if (Female)
        //    {
        //        UserGender.YourGender = Gender.Female;
        //        UserInfo.DefaultPicture = Gender.Female;
        //    }


        //    PersonList.PersonInfo.Add(_person);

        //}

        public void Disconnect()
        {
            _disconnectContent = Factory.CreateDisconnectContent();

            _disconnectContent.Id = User.Id;

            TcpSocket.tcpSocket.Send(ConverData.ToSend(_disconnectContent));

            TcpSocket.tcpSocket.Shutdown(SocketShutdown.Both);
            TcpSocket.tcpSocket.BeginDisconnect(true, new AsyncCallback(DisconnectCallback), TcpSocket.tcpSocket);

        }
        private void DisconnectCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            client.EndDisconnect(ar);
        }
    }
}
