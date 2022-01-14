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
using MVVMChatClient.Core.Model.EncryptAndDecrypt;
using MVVMChatClient.Core.Model.Authorization;
using MVVMChatClient.Core.ViewModel;

namespace MVVMChatClient.Core.Model
{
    public class Chatting : IChatting
    {
        private static CancellationTokenSource Source { get; set; }
        private static SynchronizationContext UiContext { get; set; }

        private CancellationToken token;
        private bool FirstTime { get; set; }

        private static StringBuilder TextFromServer { get; set; }

        private IWindowsViewModel _windowsViewModel;

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
            if (container.Messages == null) return;

            foreach (var item in container.Messages)
            {
                //uiContext.Send(x => MessageList.Items.Add(messageContent.NewInstance(item)), null);

                PublicChat.ChatListHolder.Content.Add(messageContent.NewInstance(item));
            }
            PublicChat.ChatListHolder.AddFirstTimeMessages(uiContext);
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
                    temporaryUserContent.UserName = container.Persons[container.Persons.Count - 1].PersonId;//.Name;
                    temporaryUserContent.PersonId = container.Persons[container.Persons.Count - 1].PersonId;
                }
                else
                {
                    if (container.Persons[container.Persons.Count - 1].Sex == 2) temporaryUserContent.UserPicture = Gender.Female;
                    if (container.Persons[container.Persons.Count - 1].Sex == 1) temporaryUserContent.UserPicture = Gender.Male;
                    temporaryUserContent.UserName = container.Persons[container.Persons.Count - 1].PersonId;//.Name;
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
                    if (item.Sex == 2)
                    {
                        temporaryUserContent.UserPicture = Gender.Female;
                        UserInfo.DefaultPicture = Gender.Female;
                    }
                    if (item.Sex == 1)
                    {
                        UserInfo.DefaultPicture = Gender.Male;
                        temporaryUserContent.UserPicture = Gender.Male;
                    }
                }

                temporaryUserContent.UserName = item.PersonId;
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
        

        public void Receiving(IWindowsViewModel windowsViewModel,
            IMessageContent messageContent,
            IJsonBaseContainer container,
            IJsonMessageContainer jsonMessageContainer)
        {
            ICredential credential = Factory.CreateCredential();

            _windowsViewModel = windowsViewModel;

            FirstTime = true;

            UiContext = SynchronizationContext.Current;

            Source = new CancellationTokenSource();

            token = Source.Token;

            Task.Run(() =>
            {


                if (CredentialConfirmation.GetPermission(UiContext, container, credential,
                    TextFromServer, _windowsViewModel))
                {
                    _windowsViewModel.ChangeView(2);

                    Security.UserPassword.Password.Clear();
                    Security.UserPassword.Password.Dispose();

                    Chat(UiContext, container, messageContent, jsonMessageContainer);

                }
                else
                {
                    _windowsViewModel.ChangeView(1);
                }

            }, token);

        }

        public static void CancelChatTask()
        {
            if(Source != null)
            {
                Source.Cancel();
            }
        }

        //private bool GetPermission(SynchronizationContext uiContext, IJsonBaseContainer container, ICredential credential)
        //{
        //    bool userExists = false;

        //    if (!LogInViewModel.FirstTimeLogin)
        //    {
        //        Connection.MakeConnection(uiContext, container, credential, _windowsViewModel);
        //    }

        //    CredentialConfirmation credentialConfirmation = new CredentialConfirmation();

        //    do
        //    {
        //        TextFromServer = Connection.ReceivData(uiContext, container, _windowsViewModel);

        //        if (!TextFromServer.ToString().Contains("Login"))
        //        {
        //            credentialConfirmation = ConverData.ToReceiv<CredentialConfirmation>(TextFromServer.ToString());
        //            userExists = credentialConfirmation.Status;

        //            LogInViewModel.RaiseEndLoadingEvent(userExists);
                  
        //        }

        //    } while (!userExists);


        //    return userExists;
        //}

        private void Chat(SynchronizationContext uiContext, IJsonBaseContainer container,
            IMessageContent messageContent, IJsonMessageContainer jsonMessageContainer)
        {
            while (Connection.Status)
            {
                //if (!FirstTime)
                    TextFromServer = Connection.ReceivData(uiContext, container, _windowsViewModel);

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

                    foreach (var item in container.Persons)
                    {
                        if(item.PersonId == User.Id)
                        {
                            if (item.Sex == 1) UserGender.YourGender = Gender.Male;
                            if (item.Sex == 2) UserGender.YourGender = Gender.Female;
                            break;
                        }
                    }

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
        /// <summary>
        /// Reset all.
        /// </summary>
        public static void RemoveAllContent()
        {
            UiContext.Send(x => MessageList.Items.Clear(), null);

            Model.PublicChat.ChatListHolder.Content.Clear();

            UiContext.Send(x => OnlineUsers.UserList.Clear(), null);
        }
    }
}
