using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public static class Factory
    {
        public static ITcpEndPoint CreateEndPoint()
        {
            return new EndPoint();
        }
        public static IMessageContent CreateMessageContent()
        {
            return new MessageContent();
        }
        public static IPerson CreatePerson()
        {
            return new Person();
        }
        public static IChatting CreateConnectingToServer()
        {
            return new Chatting();
        }
        public static IUserContent CreateUserContent()
        {
            return new UserContent();
        }
        public static IJsonContainer CreateJsonContainer()
        {
            return new JsonContainer();
        }
        public static IJsonMessageContainer CreateMessageContainer()
        {
            return new JsonMessageContainer()
            {
                Switch = new ChatSwitch(),
                Message = new MessageContent()
            };
        }
        public static IDisconnectContent CreateDisconnectContent()
        {
            return new DisconnectContent();
        }
    }
}
