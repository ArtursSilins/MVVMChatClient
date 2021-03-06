﻿using Newtonsoft.Json;
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

namespace MVVMChatClient.Core.Model
{
    public class Chatting : IChatting
    {
        private bool FirstTime { get; set; }

        private bool IsConnected { get; set; }

        private static StringBuilder TextFromServer { get; set; } 

        public string PersonInfo { get; set; }
        private ITcpEndPoint _tcpEndPoint;
        private IWindowsViewModel _windowsViewModel;

        private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        private static ManualResetEvent connectDone = new ManualResetEvent(false);

        public void Receiving(IWindowsViewModel windowsViewModel, ISignInViewModel loginViewModel, IMessageContent messageContent,
            ITcpEndPoint tcpEndPoint, IJsonContainer container)
        {

            _tcpEndPoint = tcpEndPoint;
            _windowsViewModel = windowsViewModel;

            FirstTime = true;

            IsConnected = true;

            var uiContext = SynchronizationContext.Current;

            Task.Run(() =>
            {

                ConnectingToServer();

                TcpSocket.tcpSocket.BeginSend(ConverData.ToSend(PersonList.GetPersonInfo()), 0, ConverData.ToSend(PersonList.GetPersonInfo()).Length, 0, new AsyncCallback(SendCallback), TcpSocket.tcpSocket);
                connectDone.WaitOne();

                while (IsConnected)
                {
                    _windowsViewModel.ChangeView(2);

                    var textFromServer = ReceivData(uiContext, container);

                    try
                    {
                        container = ConverData.ToReceiv<JsonContainer>(textFromServer.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                    }

                    if (FirstTime)
                    {
                        if (container.CurrentPersonId != null)
                            User.Id = container.CurrentPersonId.Id;

                        AddToOnlineUserList(uiContext, container);

                        GetAllMessages(messageContent, container, uiContext);


                        FirstTime = false;
                    }
                    else
                    {
                        // Detect if OnlineUsers changed
                        if (container?.Persons?.Count > OnlineUsers.UserList.Count || container?.Persons?.Count < OnlineUsers.UserList.Count)
                        {
                            ChangeOnlineUserList(uiContext, container, textFromServer.ToString());
                        }
                        else
                        {
                            AddNewMessage(uiContext, messageContent, textFromServer.ToString());
                        }



                    }




                }
            });

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
            connectDone.Set();
        }

        private static void GetAllMessages(IMessageContent messageContent, IJsonContainer container, SynchronizationContext uiContext)
        {
            foreach (var item in container.Messages)
            {
                uiContext.Send(x => MessageList.Items.Add(messageContent.NewInstance(item)), null);
            }
        }

        private void ChangeOnlineUserList(SynchronizationContext uiContext, IJsonContainer container, string textFromServer)
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
        private void AddToOnlineUserList(SynchronizationContext uiContext, IJsonContainer container)
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
        private void AddNewMessage(SynchronizationContext uiContext, IMessageContent messageContent, string textFromServer)
        {
            messageContent = ConverData.ToReceiv<MessageContent>(textFromServer);

            uiContext.Send(x => MessageList.Items.Add(messageContent), null);
        }

        private StringBuilder ReceivData(SynchronizationContext uiContext, IJsonContainer container)
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


        public void Receiving2(IWindowsViewModel windowsViewModel,
            IMessageContent messageContent, IJsonContainer container)
        {

            _windowsViewModel = windowsViewModel;

            FirstTime = true;

            IsConnected = true;

            var uiContext = SynchronizationContext.Current;

            Task.Run(() =>
            {

                if (GetPermission(uiContext, container))///true then next
                {
                    _windowsViewModel.ChangeView(2);

                    //while (IsConnected)
                    //{
                        Chat(uiContext, container, messageContent);
                    //}
                }
                else
                {
                    //FirstTimeLogin = false;//to isConnected class ?

                    _windowsViewModel.ChangeView(1);

                    // here some static error class witch binging to label under login panel to show message!
                }

            });

        }

        private bool GetPermission(SynchronizationContext uiContext, IJsonContainer container)
        {
            bool userExists = false;

           // if (LogInViewModel.FirstTimeLogin)
                ConnectingToServer();

            TcpSocket.tcpSocket.BeginSend(ConverData.ToSend(PersonList.GetPersonInfo()), 0, ConverData.ToSend(PersonList.GetPersonInfo()).Length, 0, new AsyncCallback(SendCallback), TcpSocket.tcpSocket);
            connectDone.WaitOne();

            TextFromServer = ReceivData(uiContext, container);

            try
            {
                container = ConverData.ToReceiv<JsonContainer>(TextFromServer.ToString());

                //if apruval ok userExists = true
                userExists = true; //just for test!!!!!!!!!!!!
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }



            return userExists;
        }
        private void Chat(SynchronizationContext uiContext, IJsonContainer container, IMessageContent messageContent)
        {
            while (IsConnected)
            {
                if (!FirstTime)
                    TextFromServer = ReceivData(uiContext, container);

                try
                {
                    container = ConverData.ToReceiv<JsonContainer>(TextFromServer.ToString());
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }

                if (FirstTime)
                {
                    if (container.CurrentPersonId != null)
                        User.Id = container.CurrentPersonId.Id;

                    AddToOnlineUserList(uiContext, container);

                    GetAllMessages(messageContent, container, uiContext);


                    FirstTime = false;
                }
                else
                {
                    // Detect if OnlineUsers changed
                    if (container?.Persons?.Count > OnlineUsers.UserList.Count || container?.Persons?.Count < OnlineUsers.UserList.Count)
                    {
                        ChangeOnlineUserList(uiContext, container, TextFromServer.ToString());
                    }
                    else
                    {
                        AddNewMessage(uiContext, messageContent, TextFromServer.ToString());
                    }

                }
            }
        }
        #region Backup
        //public void Receiving2(IWindowsViewModel windowsViewModel,
        //   IMessageContent messageContent, IJsonContainer container)
        //{
        //    FirstTime = true;

        //    Client.IsConnected = true;

        //    var uiContext = SynchronizationContext.Current;

        //    string con = "Connected";
        //    var conByte = Encoding.UTF8.GetBytes(con);

        //    TcpSocket.tcpSocket.BeginSend(conByte, 0, conByte.Length, 0, new AsyncCallback(SendCallback), TcpSocket.tcpSocket);
        //    connectDone.WaitOne();

        //    TcpSocket.tcpSocket.BeginSend(ConverData.ToSend(PersonList.GetPersonInfo()), 0, ConverData.ToSend(PersonList.GetPersonInfo()).Length, 0, new AsyncCallback(SendCallback), TcpSocket.tcpSocket);
        //    connectDone.WaitOne();

        //    while (Client.IsConnected)
        //    {
        //        _windowsViewModel.ChangeView(1);

        //        Client.TextFromServer = "";

        //        //var textFromServer = ReceivData(uiContext, container);
        //        Client.ReceiveAsync(TcpSocket.tcpSocket);
        //        Client.ReceiveDone.WaitOne();

        //        try
        //        {
        //            container = ConverData.ToReceiv<JsonContainer>(Client.TextFromServer);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.Write(ex.ToString());
        //        }

        //        if (FirstTime)
        //        {
        //            if (container.CurrentPersonId != null)
        //                User.Id = container.CurrentPersonId.Id;

        //            AddToOnlineUserList(uiContext, container);

        //            GetAllMessages(messageContent, container, uiContext);


        //            FirstTime = false;
        //        }
        //        else
        //        {
        //            // Detect if OnlineUsers changed
        //            if (container?.Persons?.Count > OnlineUsers.UserList.Count || container?.Persons?.Count < OnlineUsers.UserList.Count)
        //            {
        //                ChangeOnlineUserList(uiContext, container, Client.TextFromServer);
        //            }
        //            else
        //            {
        //                AddNewMessage(uiContext, messageContent, Client.TextFromServer);
        //            }



        //        }




        //    }

        //}
        #endregion
    }
}
