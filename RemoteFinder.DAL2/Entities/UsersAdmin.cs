using System;
using System.Collections.Generic;

namespace RemoteFinder.DAL2.Entities
{
    public partial class UsersAdmin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? LoginsCount { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
