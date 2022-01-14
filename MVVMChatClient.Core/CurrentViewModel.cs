using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMChatClient.Core
{
    public static class CurrentViewModel
    {
        public static bool LogIn { get; set; }
        public static bool SignIn { get; set; }

        public static void SetViewModel(LogInOrSignIn loginOrSignin)
        {
            if (loginOrSignin == LogInOrSignIn.LogIn)
            {
                LogIn = true;
                SignIn = false;
            }

            if (loginOrSignin == LogInOrSignIn.SignIn)
            {
                LogIn = false;
                SignIn = true;

            }
        }
        public static LogInOrSignIn GetViewModel()
        {
            if (LogIn == true)
                return LogInOrSignIn.LogIn;
            else
                return LogInOrSignIn.SignIn;
        }
    }
    public enum LogInOrSignIn
    {
        LogIn, SignIn
    }
}
