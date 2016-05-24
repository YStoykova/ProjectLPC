using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lopoca.Web.Models
{
    public class ValidateFileAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return false;
            }

            if (file.ContentLength > 4096 * 1024)
            {
                return false;
            }

            try
            {
                var validFileTypes = new string[]{
                    "text/csv",                  
                    "application/csv",
                    "text/comma-separated-values",
                    "application/excel",
                    "application/vnd.ms-excel",
                    "application/vnd.msexcel"
                   
                };
                if (validFileTypes.Contains(file.ContentType))
                {
                    return true;
                }
            }
            catch { }
            return false;
        }
    }
}