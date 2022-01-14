using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVMChatClient
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null )
            {

                //SecureString secureString = new SecureString();
                
                //secureString = ((PasswordBox)sender).SecurePassword.Length;

                //if(((PasswordBox)sender).SecurePassword.Length > 4)
                //{
                    Core.Model.Security.UserPassword.Password = new SecureString();

                    Core.Model.Security.UserPassword.Password = ((PasswordBox)sender).SecurePassword;

                    //Core.Model.Security.UserPassword.HandleSecureString(Core.Model.Security.UserPassword.Password);

                    //secureString.Clear();
                    //secureString.Dispose();

                    Core.ViewModel.LogInViewModel.PasswordReady = true;
                //}

            }

        }
    }
}
