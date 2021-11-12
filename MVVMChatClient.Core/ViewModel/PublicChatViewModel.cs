using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model;
using MVVMChatClient.Core.ViewModel.BaseClass;
using MVVMChatClient.Core.ViewModel.Commands;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel;

namespace MVVMChatClient.Core.ViewModel
{
    public class PublicChatViewModel : ViewModelBase
    {
        public ICommand _SendCommand { get; private set; }
        public ICommand _PrivateChatCommand { get; private set; }

        private IWindowsViewModel _windowsViewModel;

        private IPrivateChatViewModel _privateChatViewModel;
        private IJsonMessageContainer _jsonMessageContainer;

        private IDisconnectContent _disconnectContent;

        private string sendText;

        public string SendText
        {
            get
            {
                return sendText;
            }
            set
            {
                sendText = value;
                OnPropertyChanged(nameof(SendText));
            }
        }

        private string userPicture;

        public string UserPicture
        {
            get
            {
                if (UserInfo.AddedPicture == null)
                    return userPicture = UserInfo.DefaultPicture;
                else
                    return UserInfo.AddedPicture;
            }
            set
            {
                userPicture = value;
                OnPropertyChanged(nameof(UserPicture));
            }
        }

        private string nameText;

        public string NameText
        {
            get
            {
                return nameText = UserInfo.Name;
            }
            set
            {
                nameText = value;
                OnPropertyChanged(nameof(NameText));
            }
        }
        private UserContent privateChatPerson;

        public UserContent PrivateChatPerson
        {
            get
            {
                return privateChatPerson;
            }
            set
            {
                privateChatPerson = value;
                PrivateCahtPersonData.Get = value;
                PrivateCahtPersonData.RepeatGet = value;
                OnPropertyChanged(nameof(PrivateChatPerson));
            }
        }

        private ObservableCollection<IMessageContent> messageLists;

        public ObservableCollection<IMessageContent> MessageLists
        {
            get { return messageLists; }
            set
            {
                messageLists = value;
                OnPropertyChanged(nameof(MessageLists));
            }
        }

        private bool startUnloadAnimation;

        public bool StartUnloadAnimation
        {
            get { return startUnloadAnimation; }
            set
            {
                startUnloadAnimation = value;
                OnPropertyChanged(nameof(StartUnloadAnimation));
            }
        }



        public void Send()
        {
            _jsonMessageContainer.Switch.ChatMode = ChatMode.Public;

            _jsonMessageContainer.Message.IdList = new List<string>() { User.Id};

            _jsonMessageContainer.Message.MessageText = SendText;
            _jsonMessageContainer.Message.MessageTime = DateTime.Now.ToShortTimeString();
            _jsonMessageContainer.Message.MessageColour = SenderReceiwer.Send;

            if(!_jsonMessageContainer.Message.PictureChanged)
                _jsonMessageContainer.Message.MessagePicture = UserGender.GetUserGender();

            _jsonMessageContainer.Message.Name = UserInfo.Name;

            _jsonMessageContainer.Message.Id = User.Id;

            if (UserInfo.AddedPicture != null)
            {
                _jsonMessageContainer.Message.Pic = ConvertImage.ToByte(UserInfo.AddedPicture);
                _jsonMessageContainer.Message.MessagePicture = @"C:\Users\X\Downloads\ChatData\ChatImage" + _jsonMessageContainer.Message.Id + ".jpg";

                _jsonMessageContainer.Message.PictureChanged = true;
            }
            

            var messageInBytes = ConverData.ToSend(_jsonMessageContainer);

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

            SendText = "";

            UserInfo.AddedPicture = null;
            _jsonMessageContainer.Message.Pic = null;
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

        private void DisconnectCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            client.EndDisconnect(ar);
        }

        public PublicChatViewModel(IJsonMessageContainer messageContainer,
            IWindowsViewModel windowsViewModel,
            IPrivateChatViewModel privateChatViewModel)
        {
            _windowsViewModel = windowsViewModel;
            _jsonMessageContainer = messageContainer;
            _privateChatViewModel = privateChatViewModel;
            
            _SendCommand = new RelayCommand(Send);

            StartUnloadAnimation = true;

            PrivateChatViewModel.AllowPrivateChat = true;
            _PrivateChatCommand = new RelayCommand(_privateChatViewModel.SetPrivateChat);

            OnlineUsers.UserList = new BindingList<IUserContent>();
            
        }

        public void Disconnect()
        {
            _disconnectContent = Factory.CreateDisconnectContent();

            _disconnectContent.Id = User.Id;

            TcpSocket.tcpSocket.Send(ConverData.ToSend(_disconnectContent));

            TcpSocket.tcpSocket.Shutdown(SocketShutdown.Both);
            TcpSocket.tcpSocket.BeginDisconnect(true, new AsyncCallback(DisconnectCallback), TcpSocket.tcpSocket);

        }
       
    }
}
