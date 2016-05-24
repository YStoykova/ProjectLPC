using System;
using System.Collections.Generic;

namespace Lopoca.Data.Models
{
    public partial class StatusType
    {
        public StatusType()
        {
            this.FileHistories = new List<FileHistory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<FileHistory> FileHistories { get; set; }
    }
}
