using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Lopoca.Data.Models.Mapping;

namespace Lopoca.Data.Models
{
    public partial class LopocaDBContext : DbContext
    {
        static LopocaDBContext()
        {
            Database.SetInitializer<LopocaDBContext>(null);
        }

        public LopocaDBContext()
            : base("Name=LopocaDBContext")
        {
        }

        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<FileHistory> FileHistories { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AspNetRoleMap());
            modelBuilder.Configurations.Add(new AspNetUserClaimMap());
            modelBuilder.Configurations.Add(new AspNetUserLoginMap());
            modelBuilder.Configurations.Add(new AspNetUserMap());
            modelBuilder.Configurations.Add(new FileMap());
            modelBuilder.Configurations.Add(new FileHistoryMap());
            modelBuilder.Configurations.Add(new StatusTypeMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
        }
    }
}
