using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lopoca.Data.Models;

namespace Lopoca.Web.Models
{
    public class FileHistoryViewModel
    {
        public System.Guid FileId { get; set; }
        public System.DateTime ActionTime { get; set; }
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
        public int StatusTypeId { get; set; }
        
        public string StatusTypeName
        {
            get
            {
                return ((Lopoca.Data.Models.StatusTypes)this.StatusTypeId).ToString();
            }

        }
    }
}