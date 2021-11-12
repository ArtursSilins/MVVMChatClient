using Newtonsoft.Json;
using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using MVVMChatClient.Core.Model.ClientComunication;
using MVVMChatClient.Core.Model.EncryptAndDecrypt;
using MVVMChatClient.Core.Model.Authorization;

namespace MVVMChatClient.Core.Model
{
    public class Chatting : IChatting
    {
        private bool FirstTime { get; set; }

        private bool IsConnected { get; set; }

        private static StringBuilder TextFromServer { get; set; } 

        private IWindowsViewModel _windowsViewModel;

        private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        private static ManualResetEvent connectDone = new ManualResetEvent(false);

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
            connectDone.Set();
        }

        private static void GetAllMessages(IMessageContent messageContent, IJsonBaseContainer container, SynchronizationContext uiContext)
        {
            foreach (var item in container.Messages)
            {
                uiContext.Send(x => MessageList.Items.Add(messageContent.NewInstance(item)), null);
                PublicChat.ChatListHolder.Content.Add(messageContent.NewInstance(item));
            }
        }

        private void ChangeOnlineUserList(SynchronizationContext uiContext, IJsonBaseContainer container, string textFromServer)
        {
            UserContent temporaryUserContent = new UserContent();

            if (container.Persons.Count > OnlineUsers.UserList.Count)
            {
                if (container.Persons[container.Persons.Count - 1].Pic != null)
                {
                    ConvertImage.ByteToImage(container.Persons[container.Persons.Count - 1].Pic,
                        container.Persons[container.Persons.Count - 1].PersonId);
                    temporaryUserContent.UserPicture = container.Persons[container.Persons.Count - 1].PicturePath;
                    temporaryUserContent.UserName = container.Persons[container.Persons.Count - 1].Name;
                    temporaryUserContent.PersonId = container.Persons[container.Persons.Count - 1].PersonId;
                }
                else
                {
                    if (container.Persons[container.Persons.Count - 1].Female == true) temporaryUserContent.UserPicture = Gender.Female;
                    if (container.Persons[container.Persons.Count - 1].Male == true) temporaryUserContent.UserPicture = Gender.Male;
                    temporaryUserContent.UserName = container.Persons[container.Persons.Count - 1].Name;
                    temporaryUserContent.PersonId = container.Persons[container.Persons.Count - 1].PersonId;
                }
                              
                uiContext.Send(x => OnlineUsers.UserList.Insert(0, temporaryUserContent), null);
            }
            else
            {


                uiContext.Send(x => OnlineUsers.UserList.RemoveAt(GetIndexToRemove(container.Persons)), null);
            }
        }
        private void AddToOnlineUserList(SynchronizationContext uiContext, IJsonBaseContainer container)
        {
            foreach (var item in container.Persons)
            {
                UserContent temporaryUserContent = new UserContent();

                if (item.Pic != null)
                {
                    ConvertImage.ByteToImage(item.Pic, item.PersonId);

                    temporaryUserContent.UserPicture = item.PicturePath;
                }
                else
                {
                    if (item.Female == true) temporaryUserContent.UserPicture = Gender.Female;
                    if (item.Male == true) temporaryUserContent.UserPicture = Gender.Male;
                }

                temporaryUserContent.UserName = item.Name;
                temporaryUserContent.PersonId = item.PersonId;

                uiContext.Send(x => OnlineUsers.UserList.Insert(0, temporaryUserContent), null);
            }
        }
        private void AddNewMessage(SynchronizationContext uiContext, IJsonMessageContainer jsonMessageContainer, string textFromServer)
        {
            jsonMessageContainer = ConverData.ToReceiv<MessageContainer>(textFromServer);


            if (jsonMessageContainer.Switch.ChatMode == ChatMode.Public)
            {
                uiContext.Send(x => PublicChat.ChatListHolder.Content.Add(jsonMessageContainer.Message), null);

                if(CurrentChatMode.Mode == ChatMode.Public)
                {
                    uiContext.Send(x => MessageList.Items.Add(jsonMessageContainer.Message), null);
                }

            }
            else if(jsonMessageContainer.Switch.ChatMode == ChatMode.Private)
            {
                uiContext.Send(x => PrivateChat.ChatListHolder.AddToPersonChat(jsonMessageContainer.Message.IdList,
                    jsonMessageContainer.Message), null);

                uiContext.Send(x => OnlineUsersList.Notifications.Add(), null);

                if (CurrentChatMode.Mode == ChatMode.Private)
                {
                    OnlineUsersList.Notifications.Remove();

                    uiContext.Send(x => MessageList.Items.Add(jsonMessageContainer.Message), null);
                }
            }

        }

        private StringBuilder ReceivData(SynchronizationContext uiContext, IJsonBaseContainer container)
        {

            var buffer = new byte[256];

            int size = 0;

            var textFromServer = new StringBuilder();

            do
            {
                try
                {
                    size = TcpSocket.tcpSocket.Receive(buffer);
                }
                catch (Exception ex)
                {
                    AlertMessages.Show(ex.Message);

                    uiContext.Send(x=> OnlineUsers.UserList.Clear(), null);

                    container?.Persons?.Clear();

                    TcpSocket.tcpSocket.Shutdown(SocketShutdown.Both);

                    TcpSocket.tcpSocket.BeginDisconnect(true, new AsyncCallback(DisconnectCallback), TcpSocket.tcpSocket);


                    IsConnected = false;

                    _windowsViewModel.ChangeView(1);

                }
                

                textFromServer.Append(Encoding.UTF8.GetString(buffer, 0, size));

            } while (TcpSocket.tcpSocket.Available > 0);

            return textFromServer;
        }
        private static void DisconnectCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            client.EndDisconnect(ar);
        }

        private int GetIndexToRemove( List<Person> serverPersons )
        {
            int indexCount = -1;
            bool isMatch = false;

            foreach (var item in OnlineUsers.UserList)
            {
                indexCount++;
                isMatch = false;

                foreach (var item2 in serverPersons)
                {
                    if (item.PersonId == item2.PersonId) isMatch = true;
                }

                if (isMatch == false) break;
            }

            return indexCount;
        }
        private void ConnectingToServer()
        {
            try
            {
                EndPoint endPoint = new EndPoint();

                TcpSocket.tcpSocket.BeginConnect(endPoint.TcpEndPoint, new AsyncCallback(ConnectCallback), TcpSocket.tcpSocket);
                connectDone.WaitOne();


                Connection.Status = true;
            }
            catch (SocketException ex)
            {
                Connection.Status = false;
                _windowsViewModel.ChangeView(0);
                return;
            }
            catch (Exception ex)
            {

            }

        }

        private void ConnectCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            try
            {
                client.EndConnect(ar);
            }
            catch (Exception)
            {
                AlertMessages.Show("Server not available! :( We are working on this issue, please try again later.");
            }
            
            connectDone.Set();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///


        public void Receiving(IWindowsViewModel windowsViewModel,
            IMessageContent messageContent,
            IJsonBaseContainer container,
            IJsonMessageContainer jsonMessageContainer
            , ICredential credential)
        {

            _windowsViewModel = windowsViewModel;

            FirstTime = true;

            IsConnected = true;

            var uiContext = SynchronizationContext.Current;

            Task.Run(() =>
            {


                if (GetPermission(uiContext, container, credential))///true then next
                {
                    _windowsViewModel.ChangeView(2);

                    Chat(uiContext, container, messageContent, jsonMessageContainer);

                }
                else
                {

                    _windowsViewModel.ChangeView(1);

                    // here some static error class witch binding to label under login panel to show message!
                }

            });

        }

        private bool GetPermission(SynchronizationContext uiContext, IJsonBaseContainer container, ICredential credential)
        {
            bool userExists = false;

                ConnectingToServer();

            // send public key
            AsymmetricEncryption.CreateKey("Client");

            credential.NeedAction = true;
            credential.NeedKeys = true;
            credential.PubKey = AsymmetricEncryption.PubKeyString("Client");
            
            TcpSocket.tcpSocket.BeginSend(ConverData.SendPubKey(credential), 0,
                ConverData.SendPubKey(credential).Length, 0, new AsyncCallback(SendCallback), TcpSocket.tcpSocket);


            //TcpSocket.tcpSocket.BeginSend(ConverData.ToSend(PersonList.GetPersonInfo()), 0, ConverData.ToSend(PersonList.GetPersonInfo()).Length, 0, new AsyncCallback(SendCallback), TcpSocket.tcpSocket);
            connectDone.WaitOne();

            TextFromServer = ReceivData(uiContext, container);

            try
            {
                credential = ConverData.ReceivKeys<Credential>(TextFromServer.ToString());

                credential.IV = AsymmetricEncryption.DecryptWithPrivateKey(credential.IV);
                credential.SymmetricKey = AsymmetricEncryption.DecryptWithPrivateKey(credential.SymmetricKey);

                KeyContainer.SymmetricKey.UserKeys = new System.Security.Cryptography.RijndaelManaged();
                KeyContainer.SymmetricKey.UserKeys.Key = Convert.FromBase64String(credential.SymmetricKey);
                KeyContainer.SymmetricKey.UserKeys.IV = Convert.FromBase64String(credential.IV);

                credential.IV = null;
                credential.SymmetricKey = null;

                //if apruval ok userExists = true
                userExists = false; //just for test!!!!!!!!!!!!
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }



            return userExists;
        }
        private void Chat(SynchronizationContext uiContext, IJsonBaseContainer container,
            IMessageContent messageContent, IJsonMessageContainer jsonMessageContainer)
        {
            while (IsConnected)
            {
                if (!FirstTime)
                    TextFromServer = ReceivData(uiContext, container);

                try
                {
                    container = ConverData.ToReceiv<BaseContainer>(TextFromServer.ToString());
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }

                if (FirstTime)
                {
                    if (container.CurrentPersonId != null)
                        User.Id = container.CurrentPersonId;

                    AddToOnlineUserList(uiContext, container);

                    GetAllMessages(messageContent, container, uiContext);


                    FirstTime = false;
                }
                else
                {
                    // Detect if OnlineUsers changed
                    if (container?.Persons?.Count > OnlineUsers.UserList.Count || container?.Persons?.Count < OnlineUsers.UserList.Count)
                        ChangeOnlineUserList(uiContext, container, TextFromServer.ToString());
                    else
                        AddNewMessage(uiContext, jsonMessageContainer, TextFromServer.ToString());

                }
            }
        }
    }
}
