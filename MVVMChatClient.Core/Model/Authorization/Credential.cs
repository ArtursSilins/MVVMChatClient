using MVVMChatClient.Core.Interfaces;

namespace MVVMChatClient.Core.Model.Authorization
{
    public class Credential : ICredential
    {
        public bool Login { get; set; }
        public bool SignIn { get; set; }
        public bool NeedKeys { get; set; }
        public bool NeedAction { get; set; }
        public bool PasswordConfirmed { get; set; }
        public string PubKey { get; set; }
        public string SymmetricKey { get; set; }
        public string IV { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Sex { get; set; }
    }
}
