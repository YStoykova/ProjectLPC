using System;
using System.Collections.Generic;

namespace Lopoca.Data.Models
{
    public partial class File
    {
        public File()
        {
            this.FileHistories = new List<FileHistory>();
        }

        public System.Guid Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsDeleted { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<FileHistory> FileHistories { get; set; }
    }
}
