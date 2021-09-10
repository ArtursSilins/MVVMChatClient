using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    static class PrivateCahtPersonData
    {
        public static UserContent Get { get; set; }
        /// <summary>
        /// Saves person data if the same person is picked twice or more times
        /// </summary>
        public static UserContent RepeatGet { get; set; }
    }
}
