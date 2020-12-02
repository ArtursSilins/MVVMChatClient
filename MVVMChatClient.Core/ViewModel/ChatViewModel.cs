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

namespace MVVMChatClient.Core.ViewModel
{
    public class ChatViewModel : ViewModelBase
    {
        public ICommand _SendCommand { get; private set; }

        private IWindowsViewModel _windowsViewModel;

        private IMessageContent _messageContent;

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


        public void Send()
        {
            _messageContent.MessageText = SendText;
            _messageContent.MessageTime = DateTime.Now.ToShortTimeString();           
            _messageContent.MessageColour = SenderReceiwer.Send;

            if(!_messageContent.PictureChanged)
            _messageContent.MessagePicture = UserGender.GetUserGender();

            _messageContent.Name = UserInfo.Name;

            _messageContent.Id = User.Id;

            if (UserInfo.AddedPicture != null)
            {
                _messageContent.Pic = ConvertImage.ToByte(UserInfo.AddedPicture);
                _messageContent.MessagePicture = @"C:\Users\X\Downloads\ChatData\ChatImage" + _messageContent.Id + ".jpg";

                _messageContent.PictureChanged = true;
            }
            

            var messageInBytes = ConverData.ToSend(_messageContent);

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
            _messageContent.Pic = null;
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

        public ChatViewModel(IMessageContent messageContent, IWindowsViewModel windowsViewModel)
        {
            _windowsViewModel = windowsViewModel;

            _messageContent = messageContent;

            _SendCommand = new RelayCommand(Send);

            OnlineUsers.UserList = new ObservableCollection<IUserContent>();

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
