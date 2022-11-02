using System;
using System.Collections.Generic;

namespace RemoteFinder.DAL2.Entities
{
    public partial class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string OrderId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UserSocialId { get; set; }

        public virtual UsersSocial UserSocial { get; set; }
    }
}
