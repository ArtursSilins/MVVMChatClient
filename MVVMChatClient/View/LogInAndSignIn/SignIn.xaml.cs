using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
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
    public partial class SignIn : UserControl
    {

        private SecureString Password1 { get; set; }
        private SecureString Password2 { get; set; }
        public SignIn()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {

                //SecureString secureString = new SecureString();

                //secureString = ((PasswordBox)sender).SecurePassword.Length;

                //if(((PasswordBox)sender).SecurePassword.Length > 4)
                //{
                Password1 = new SecureString();
                Password1 = ((PasswordBox)sender).SecurePassword;

                Core.Model.Security.UserPassword.Password = new SecureString();

                Core.Model.Security.UserPassword.Password = ((PasswordBox)sender).SecurePassword;

                //Core.Model.Security.UserPassword.HandleSecureString(Core.Model.Security.UserPassword.Password);

                //secureString.Clear();
                //secureString.Dispose();

                Core.ViewModel.LogInViewModel.PasswordReady = true;

                Core.ViewModel.SignInViewModel.EmptyPassword = false;

            }
        }

        private void PasswordBox_PasswordChanged_1(object sender, RoutedEventArgs e)
        {
            Password2 = new SecureString();
            Password2 = ((PasswordBox)sender).SecurePassword;

            if(Core.Model.Security.UserPassword.IsEqual(Password1, Password2))
            {
                Core.Model.Security.UserPassword.Password = new SecureString();

                Core.Model.Security.UserPassword.Password = ((PasswordBox)sender).SecurePassword;

                Core.ViewModel.SignInViewModel.PasswordReady = true;

                Core.ViewModel.SignInViewModel.EmptyPassword = false;
            }
            else
            {
                Core.ViewModel.SignInViewModel.PasswordReady = false;
            }
        }
    }
}
