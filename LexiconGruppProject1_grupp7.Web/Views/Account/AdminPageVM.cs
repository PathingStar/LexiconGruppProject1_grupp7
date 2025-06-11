using System.ComponentModel.DataAnnotations;

namespace LexiconGruppProject1_grupp7.Web.Views.Account
{
    public class AdminPageVM
    {
        public UserVM[] userVMs { get; set; }

        public class UserVM
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public bool AdminAccess { get; set; } = false;
        }
    }
}
