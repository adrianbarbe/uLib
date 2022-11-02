using System;
using System.Collections.Generic;

namespace RemoteFinder.DAL2.Entities
{
    public partial class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public decimal FileSize { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserSocialId { get; set; }

        public virtual UsersSocial UserSocial { get; set; }
    }
}
