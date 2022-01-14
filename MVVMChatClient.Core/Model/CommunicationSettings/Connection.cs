using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model.Authorization;
using MVVMChatClient.Core.Model.ChatSwitching;
using MVVMChatClient.Core.Model.EncryptAndDecrypt;
using MVVMChatClient.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public static class Connection
    {
        public static bool ConnectionFail { get; set; } 

        private static ManualResetEvent connectDone = new ManualResetEvent(false);

        private static IWindowsViewModel _windowsViewModel;
        public static bool Status { get; set; }
        /// <summary>
        /// Prepare all for client and server communication.
        /// </summary>
        /// <param name="uiContext"></param>
        /// <param name="container"></param>
        /// <param name="credential"></param>
        /// <param name="windowsViewModel"></param>
        public static void MakeConnection(SynchronizationContext uiContext,
            IJsonBaseContainer container,
            ICredential credential,
            IWindowsViewModel windowsViewModel)
        {
            _windowsViewModel = windowsViewModel;

            

            BeginConnectToServer(windowsViewModel);

            if(ConnectionFail != true)
                SetUpEncryptions(uiContext, container, credential, windowsViewModel);

        }
        /// <summary>
        /// Make a"secure" key exchange (without certificates)
        /// </summary>
        /// <param name="uiContext"></param>
        /// <param name="container"></param>
        /// <param name="credential"></param>
        /// <param name="windowsViewModel"></param>
        private static void SetUpEncryptions(SynchronizationContext uiContext,
            IJsonBaseContainer container, 
            ICredential credential,
            IWindowsViewModel windowsViewModel)
        {

            LogInViewModel.FirstTimeLogin = true;

            // send public key
            AsymmetricEncryption.CreateKey("Client");

            credential.NeedAction = true;
            credential.NeedKeys = true;
            credential.PubKey = AsymmetricEncryption.PubKeyString("Client");

            TcpSocket.tcpSocket.BeginSend(ConverData.SendPubKey(credential), 0,
                ConverData.SendPubKey(credential).Length, 0, new AsyncCallback(SendCallback), TcpSocket.tcpSocket);


            connectDone.WaitOne();

            StringBuilder textFromServer = new StringBuilder();

            textFromServer = ReceivData(uiContext, container, windowsViewModel);

            try
            {
                credential = ConverData.ReceivKeys<Credential>(textFromServer.ToString());

                credential.IV = AsymmetricEncryption.DecryptWithPrivateKey(credential.IV);
                credential.SymmetricKey = AsymmetricEncryption.DecryptWithPrivateKey(credential.SymmetricKey);

                KeyContainer.SymmetricKey.UserKeys = new System.Security.Cryptography.RijndaelManaged();
                KeyContainer.SymmetricKey.UserKeys.Key = Convert.FromBase64String(credential.SymmetricKey);
                KeyContainer.SymmetricKey.UserKeys.IV = Convert.FromBase64String(credential.IV);

                credential.IV = null;
                credential.SymmetricKey = null;

                if(CurrentViewModel.LogIn)
                    LogInViewModel.RaiseSendDataEvent();
                if (CurrentViewModel.SignIn)
                    SignInViewModel.RaiseSendDataEvent();


            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }
        private static void SendCallback(IAsyncResult ar)
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
        public static StringBuilder ReceivData(SynchronizationContext uiContext,
            IJsonBaseContainer container, IWindowsViewModel windowsViewModel)
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

                    Chatting.RemoveAllContent();

                    EndConnection(windowsViewModel, ChangeViewTo.LogIn);

                }


                textFromServer.Append(Encoding.UTF8.GetString(buffer, 0, size));

            } while (TcpSocket.tcpSocket.Available > 0);

            return textFromServer;
        }
        /// <summary>
        /// Connect to server.
        /// </summary>
        /// <param name="windowsViewModel"></param>
        private static void BeginConnectToServer(IWindowsViewModel windowsViewModel)
        {
            try
            {
                EndPoint endPoint = new EndPoint();

                TcpSocket.tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                TcpSocket.tcpSocket.BeginConnect(endPoint.TcpEndPoint, new AsyncCallback(ConnectCallback), TcpSocket.tcpSocket);
                connectDone.WaitOne();


                Status = true;
            }
            catch (SocketException ex)
            {
                Status = false;
                windowsViewModel.ChangeView(0);
                return;
            }
            catch (Exception ex)
            {

            }

        }
        private static void ConnectCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            try
            {
                client.EndConnect(ar);
            }
            catch (Exception)
            {
                AlertMessages.Show("Server not available! :( We are working on this issue, please try again later.");

                ConnectionFail = true;

                Status = false;

                LogInViewModel.FirstTimeLogin = false;

                ////MessageAddControl.ResetData();

                ////Chatting.RemoveAllContent();

                LogInViewModel.RaiseEndLoadingEvent(true);
            }

            connectDone.Set();
        }

        public static void EndConnection(IWindowsViewModel windowsViewModel, ChangeViewTo changeViewTo)
        {
            windowsViewModel.ChangeView((int)changeViewTo);

            Chatting.CancelChatTask();

            LogInViewModel.FirstTimeLogin = false;

            Disconnect();
        }

        private static void Disconnect()
        {
            Status = false;

            IDisconnectContent _disconnectContent = Factory.CreateDisconnectContent();

            _disconnectContent.Id = User.Id;

            TcpSocket.tcpSocket.Send(ConverData.ToSend(_disconnectContent));

            TcpSocket.tcpSocket.Shutdown(SocketShutdown.Both);
            TcpSocket.tcpSocket.BeginDisconnect(true, new AsyncCallback(DisconnectCallback), TcpSocket.tcpSocket);
        }
        private static void DisconnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndDisconnect(ar);


            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public enum ChangeViewTo
        {
            SignIn, LogIn
        }
    }
}
