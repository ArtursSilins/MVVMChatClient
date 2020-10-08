using MVVMChatClient.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model
{
    public static class AlertMessages
    {
        public static IAlertMessages alertMessages { get; set; }

        public static void Show(string exception)
        {
            alertMessages.Show(exception);
        }
    }
}
