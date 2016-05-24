using System;
using System.Collections.Generic;

namespace Lopoca.Data.Models
{
    public partial class FileHistory
    {
        public int Id { get; set; }
        public System.Guid FileId { get; set; }
        public System.DateTime ActionTime { get; set; }
        public string UserId { get; set; }
        public int StatusTypeId { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual File File { get; set; }
        public virtual StatusType StatusType { get; set; }
    }
}
