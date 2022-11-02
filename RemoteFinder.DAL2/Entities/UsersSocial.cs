using System;
using System.Collections.Generic;

namespace RemoteFinder.DAL2.Entities
{
    public partial class UsersSocial
    {
        public UsersSocial()
        {
            Files = new HashSet<File>();
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserPicture { get; set; }
        public string ProviderUserId { get; set; }
        public int OauthProvider { get; set; }

        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
