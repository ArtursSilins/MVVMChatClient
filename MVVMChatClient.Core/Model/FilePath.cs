using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public static class FilePath
    {
        public static IFilePath filePath { get; set; }

        public static string Get()
        {
            return filePath.Get();
        }
    }
}
