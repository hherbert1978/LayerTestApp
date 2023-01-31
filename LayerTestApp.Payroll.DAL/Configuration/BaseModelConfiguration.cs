using LayerTestApp.Payroll.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerTestApp.Payroll.DAL.Configuration
{
    public  class BaseModelConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseModel
    {
        public virtual void Configure(EntityTypeBuilder<T> entity)
        {
            entity.Property(q => q.IsActive)
              .HasColumnName("is_active")
              .HasDefaultValue(true)
              .IsRequired(true);

            entity.Property(q => q.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false)
                .IsRequired(true);

            entity.Property(q => q.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired(true);

            entity.Property(q => q.LastUpdatedAt)
                .HasColumnName("last_updated_at")
                .IsRequired(false);

            entity.Property(q => q.DeletedAt)
                 .HasColumnName("deleted_at")
                 .IsRequired(false);
        }
    }
}


