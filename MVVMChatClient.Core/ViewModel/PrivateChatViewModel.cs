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

        private DispatcherTimer MessageDelayTimer;

        private string sendText;

        /// <summary>
        /// Disable two time click while loading private chat.
        /// </summary>
        public static bool AllowPrivateChat { get; set; }

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
                //if (UserInfo.AddedPicture == null)
                //    return userPicture = UserInfo.DefaultPicture;
                //else
                //    return UserInfo.AddedPicture;
                return userPicture;
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
                return nameText;
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

            Model.ChatSwitching.MessageAddControl.ResetData();

            Model.PublicChat.ChatListHolder.AddMessagesFirstTime();

            _windowsViewModel.ChangeView(2);
        }

        public void SetPrivateChat()
        {
            if(AllowPrivateChat)
            {
                AllowPrivateChat = false;

                Timer = new DispatcherTimer();
                Timer = new DispatcherTimer(DispatcherPriority.SystemIdle);
                Timer.Interval = TimeSpan.FromMilliseconds(0);
                Timer.Tick += Timer_Tick;
                Timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (PrivateCahtPersonData.RepeatGet != null)
            {
                CurrentChatMode.Mode = ChatMode.Private;

                Model.OnlineUsersList.Notifications.Id = PrivateCahtPersonData.RepeatGet.PersonId;

                Model.OnlineUsersList.Notifications.Remove();

                Model.ChatSwitching.MessageAddControl.ResetData();

                User.PrivatePersonId = PrivateCahtPersonData.RepeatGet.PersonId;

                UserPicture = PrivateCahtPersonData.RepeatGet.UserPicture;

                NameText = PrivateCahtPersonData.RepeatGet.UserName;

                _windowsViewModel.ChangeView(3);

                MessageList.Items.Clear();

                MessageDelayTimer = new DispatcherTimer();
                MessageDelayTimer = new DispatcherTimer(DispatcherPriority.SystemIdle);
                MessageDelayTimer.Interval = TimeSpan.FromMilliseconds(400);
                MessageDelayTimer.Tick += MessageDelayTimer_Tick;
                MessageDelayTimer.Start();

                PrivateCahtPersonData.Get = null;

                AllowPrivateChat = true;

                Timer.Stop();
            }
        }

        private void MessageDelayTimer_Tick(object sender, EventArgs e)
        {
            Model.PrivateChat.ChatListHolder.GetPersonChatFirstTime(PrivateCahtPersonData.RepeatGet.PersonId);
            MessageDelayTimer.Stop();
        }

        public void Send()
        {
            _jsonMessageContainer.Switch.ChatMode = ChatMode.Private;

            _jsonMessageContainer.Message.IdList = new List<string>() { User.Id, User.PrivatePersonId };

            _jsonMessageContainer.Message.MessageText = SendText;

            _jsonMessageContainer.Message.MessageTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
