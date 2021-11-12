using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public class UserContent : IUserContent
    {
        private string userName;
        public string UserName
        {
            get
            {
                if(userName == null)
                {

                }
                return userName;
            }
            set { userName = value; }
        }

        public string UserPicture { get; set; }
        public string PersonId { get; set; }
        public int NotificationText { get; set; }
        public string NotificationVisibility { get; set; }
        public int FontSize { get => Size(UserName.Length); }      

        public int Size(int length)
        {
            int size;

            //if (length > 6)
            //    return size = 15;
            //else
                return size = 25;
        }
        private string CheckIfEmty(string value)
        {
            return "";
        }
    }
}
