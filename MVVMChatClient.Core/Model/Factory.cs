using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.Model.Authorization;
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
        public static ICredential CreateCredential()
        {
            return new Credential();
        }
        public static IJsonBaseContainer CreateJsonContainer()
        {
            return new BaseContainer();
        }
        public static IJsonMessageContainer CreateMessageContainer()
        {
            return new MessageContainer()
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
