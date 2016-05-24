using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lopoca.Web.Models
{
    public class FileViewModel
    {
        public System.Guid Id { get; set; }
        public string UserId { get; set; }
        public string UserName 
        { 
            get 
            {
                var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new UserManager<ApplicationUser>(store);
                ApplicationUser user = userManager.FindById(this.UserId);
                return user.Email;
            } 
        }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDeleteAllowed 
        { 
            get
            {
                var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new UserManager<ApplicationUser>(store);
                var roles = userManager.GetRoles(this.UserId);
               
                if (roles.Contains("Admin"))
                { 
                    return true; 
                }               
                return false;
            }
  
        }
        public bool IsUserNameDisplay { get { return false; } }
    }
}