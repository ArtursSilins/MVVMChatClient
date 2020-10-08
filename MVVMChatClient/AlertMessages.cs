using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMChatClient
{
    public class AlertMessages : Core.Interfaces.IAlertMessages
    {
        public void Show(string exception)
        {            
            MessageBox.Show(exception, "Aelert Message", MessageBoxButton.OK);
        }
    }
}
