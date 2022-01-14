using MVVMChatClient.Core.Interfaces;
using MVVMChatClient.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MVVMChatClient.Core.Model.Authorization
{
    public class CredentialConfirmation : ICredentialConfirmation
    {
        public bool Status { get; set; }
        public bool Name { get; set; }
        public bool Email { get; set; }
        public bool Login { get; set; }
        public bool SignIn { get; set; }

        public static bool GetPermission(SynchronizationContext uiContext,
            IJsonBaseContainer container,
            ICredential credential,
            StringBuilder textFromServer,
            IWindowsViewModel _windowsViewModel)
        {
            bool userExists = false;

            Connection.ConnectionFail = false;

            if (!Connection.Status)
            {
                Connection.MakeConnection(uiContext, container, credential, _windowsViewModel);
            }

            if (Connection.ConnectionFail)
            {
                Connection.Status = false;
                return false;
            }

            CredentialConfirmation credentialConfirmation = new CredentialConfirmation();

            do
            {
                textFromServer = Connection.ReceivData(uiContext, container, _windowsViewModel);
                credentialConfirmation = ConverData.ToReceiv<CredentialConfirmation>(textFromServer.ToString());

                if (credentialConfirmation.Login)
                {
                    userExists = credentialConfirmation.Status;

                    LogInViewModel.RaiseEndLoadingEvent(userExists);
                }

                if (credentialConfirmation.SignIn)
                {
                    SignInViewModel.RaiseEndLoadingEvent(credentialConfirmation);

                    if (credentialConfirmation.Status)
                    {
                        userExists = true;
                        Connection.EndConnection(_windowsViewModel, Connection.ChangeViewTo.SignIn);

                        SignInViewModel.SetControlsVisibility("hidden");
                    }
                }

            } while (!userExists);

            return userExists;
        }

    }
}
