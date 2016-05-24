using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Lopoca.Data.Models.Mapping
{
    public class FileHistoryMap : EntityTypeConfiguration<FileHistory>
    {
        public FileHistoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.UserId)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("FileHistory");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FileId).HasColumnName("FileId");
            this.Property(t => t.ActionTime).HasColumnName("ActionTime");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.StatusTypeId).HasColumnName("StatusTypeId");

            // Relationships
            this.HasRequired(t => t.AspNetUser)
                .WithMany(t => t.FileHistories)
                .HasForeignKey(d => d.UserId);
            this.HasRequired(t => t.File)
                .WithMany(t => t.FileHistories)
                .HasForeignKey(d => d.FileId);
            this.HasRequired(t => t.StatusType)
                .WithMany(t => t.FileHistories)
                .HasForeignKey(d => d.StatusTypeId);

        }
    }
}
