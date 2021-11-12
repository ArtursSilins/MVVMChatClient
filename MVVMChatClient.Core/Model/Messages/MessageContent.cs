using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public class MessageContent:IMessageContent
    {
        public string MessageVisibility { get; set; }
        public string Name { get; set; }

        public string MessageText { get; set; }

        public string MessageTime { get; set; }

        public string MessageAlignment { get; set; }

        public string MessagePictureVisibility { get; set; }

        public string MessageColour { get; set; }

        public string MessagePicture { get; set; }
        public byte[] Pic { get; set; }
        public bool PictureChanged { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// A list of people for whom the message is intended.
        /// </summary>
        public List<string> IdList { get; set; }


        public IMessageContent NewInstance(IMessageContent from)
        {
            MessageContent content = new MessageContent();

            content.MessageAlignment = from.MessageAlignment;
            content.MessageColour = from.MessageColour;
            content.MessagePicture = from.MessagePicture;
            content.MessageText = from.MessageText;
            content.MessageTime = from.MessageTime;
            content.Name = from.Name;
            content.MessagePictureVisibility = from.MessagePictureVisibility;
            content.Pic = from.Pic;
            content.PictureChanged = from.PictureChanged;
            content.Id = from.Id;
            content.IdList = new List<string>(from.IdList);

            return content;
        }
    }
}
