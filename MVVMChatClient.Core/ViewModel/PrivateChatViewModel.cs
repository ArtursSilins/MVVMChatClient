using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model;
using MVVMChatClient.Core.Model.PrivateChat;
using MVVMChatClient.Core.ViewModel.BaseClass;
using MVVMChatClient.Core.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace MVVMChatClient.Core.ViewModel
{
    public class PrivateChatViewModel : ViewModelBase, IPrivateChatViewModel
    {
        public ICommand _SendCommand { get; private set; }
        public ICommand _GoBack { get; set; }

        private IWindowsViewModel _windowsViewModel;

        private IJsonMessageContainer _jsonMessageContainer;

        private IDisconnectContent _disconnectContent;

        private DispatcherTimer Timer;

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

        public PrivateChatViewModel(IJsonMessageContainer messageContainer,
                        IWindowsViewModel windowsViewModel)
        {
            _windowsViewModel = windowsViewModel;

            _jsonMessageContainer = messageContainer;

            _SendCommand = new RelayCommand(Send);

            _GoBack = new RelayCommand(GetPublicChat);
        }

        private void GetPublicChat()
        {
            CurrentChatMode.Mode = ChatMode.Public;

            MessageList.Items.Clear();

            Model.PublicChat.ChatListHolder.AddMessagesFirstTime();

            _windowsViewModel.ChangeView(2);
        }

        public void SetPrivateChat()
        {
            Timer = new DispatcherTimer();
            Timer = new DispatcherTimer(DispatcherPriority.SystemIdle);
            Timer.Interval = TimeSpan.FromMilliseconds(300);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (PrivateCahtPersonData.RepeatGet != null)
            {
                CurrentChatMode.Mode = ChatMode.Private;
                MessageList.Items.Clear();

                Model.PublicChat.ChatListHolder.RessetMessageCount();

                Model.PrivateChat.ChatListHolder.GetPersonChat(PrivateCahtPersonData.RepeatGet.PersonId);
                //MessageList.Items = ChatListHolder.GetPersonChat(PrivateCahtPersonData.RepeatGet);
                _windowsViewModel.ChangeView(3);
                Timer.Stop();

                PrivateCahtPersonData.Get = null;
            }
        }

        public void Send()
        {
            _jsonMessageContainer.Switch.ChatMode = ChatMode.Private;

            _jsonMessageContainer.Message.MessageText = SendText;
            _jsonMessageContainer.Message.MessageTime = DateTime.Now.ToShortTimeString();
            _jsonMessageContainer.Message.MessageColour = SenderReceiwer.Send;

            if (!_jsonMessageContainer.Message.PictureChanged)
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
