using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public static class User
    {
        /// <summary>
        /// Current user Id.
        /// </summary>
        public static string Id { get; set; }
        /// <summary>
        /// Private Chat person Id.
        /// </summary>
        public static string PrivatePersonId { get; set; }
    }
}
