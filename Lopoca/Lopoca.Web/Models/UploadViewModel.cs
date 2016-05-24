using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Lopoca.Web.Models
{
    public class UploadViewModel
    {
        [StringLength(128)]
        public string FileName { get; set; }

        [Required] 
        [DataType(DataType.Upload)]
        [ValidateFile(ErrorMessage = "Please select a CSV file and the file should be smaller than 4 MB")]
        public HttpPostedFileBase File { get; set; }

        public Guid FileId { get; set; }      
        public string FullPath { get; set; }
        public string UserId { get; set; }       
    }
}