using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient
{
    public class FilePath : Core.Interfaces.IFilePath
    {
        public string Get()
        {
            string path = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();

            if(openFileDialog.ShowDialog()== true)
            {
                path = openFileDialog.FileName;
            };
            return path;
        }
    }
}
