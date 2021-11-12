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

                SecureString secureString = new SecureString();
                
                secureString = ((PasswordBox)sender).SecurePassword;

                if(secureString.Length > 4)
                {
                    Core.Model.Security.UserPassword.Password = new SecureString();

                    Core.Model.Security.UserPassword.Password = secureString;

                    Core.Model.Security.UserPassword.HandleSecureString(Core.Model.Security.UserPassword.Password);

                    secureString.Clear();
                    secureString.Dispose();

                    Core.ViewModel.LogInViewModel.PasswordReady = true;
                }
                
            }



                        // This all is done in the server side!!! //

            //PBKDF2 pBKDF2 = new PBKDF2("Password",24,90000,"SHA256");
            //pBKDF2.GetBytes(24);

            //Salt
            //private static string CreateSalt(int size)
            //{
            //    //Generate a cryptographic random number.
            //    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            //    byte[] buff = new byte[size];
            //    rng.GetBytes(buff);
            //
            //    // Return a Base64 string representation of the random number.
            //    return Convert.ToBase64String(buff);
            //}

            //PasswordSaltedHash
            //public static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
            //{
            //    HashAlgorithm algorithm = new SHA256Managed();
            //
            //    byte[] plainTextWithSaltBytes =
            //      new byte[plainText.Length + salt.Length];
            //
            //    for (int i = 0; i < plainText.Length; i++)
            //    {
            //        plainTextWithSaltBytes[i] = plainText[i];
            //    }
            //    for (int i = 0; i < salt.Length; i++)
            //    {
            //        plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            //    }
            //
            //    return algorithm.ComputeHash(plainTextWithSaltBytes);
            //}

            //CompareTwoHashedPasswords
            //public static bool CompareByteArrays(byte[] array1, byte[] array2)
            //{
            //    if (array1.Length != array2.Length)
            //    {
            //        return false;
            //    }
            //
            //    for (int i = 0; i < array1.Length; i++)
            //    {
            //        if (array1[i] != array2[i])
            //        {
            //            return false;
            //        }
            //    }
            //
            //    return true;
            //}
        }
    }
}
