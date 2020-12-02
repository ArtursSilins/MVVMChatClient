using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model.ClientComunication
{
    public static class Client
    {
        public static bool IsConnected { get; set; }

        public static string TextFromServer { get; set; }

        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        public static ManualResetEvent ReceiveDone = new ManualResetEvent(false);

        //public static Action ConnectExceptionAction;
        public static Action ReceiveExceptionAction;

        public static void Connect(Action ConnectExceptionAction)
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
                //Connection.Status = false;
                ConnectExceptionAction?.Invoke();
                IsConnected = false;
                //_windowsViewModel.ChangeView(0);
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
            }

            connectDone.Set();
        }

        public static void SendAsync(object data)
        {
            TcpSocket.tcpSocket.BeginSend(ConverData.ToSend(data), 0, ConverData.ToSend(data).Length, 0, new AsyncCallback(SendCallback), TcpSocket.tcpSocket);
            connectDone.WaitOne();
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

        public static StringBuilder Receive()
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

                    TcpSocket.tcpSocket.Shutdown(SocketShutdown.Both);

                    TcpSocket.tcpSocket.BeginDisconnect(true, new AsyncCallback(DisconnectCallback), TcpSocket.tcpSocket);


                    IsConnected = false;

                    ReceiveExceptionAction?.Invoke();
                    //_windowsViewModel.ChangeView(0);

                }


                textFromServer.Append(Encoding.UTF8.GetString(buffer, 0, size));

            } while (TcpSocket.tcpSocket.Available > 0);

            return textFromServer;
        }
        public static void ReceiveAsync(Socket client)
        {
            try
            {
                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;
                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);
                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    //  Get the rest of the data.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    if (state.sb.Length > 1)
                    {
                        var textFromServer = state.sb.ToString();
                    }
                    // Signal that all bytes have been received.  
                    ReceiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void DisconnectCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            client.EndDisconnect(ar);
        }
       
    }
}
