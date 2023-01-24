using LayerTestApp.Payroll.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LayerTestApp.Payroll.DAL.Configuration
{
    public class PayGradeConfiguration
    {
        public PayGradeConfiguration(EntityTypeBuilder<PayGradeDAL> entity)
        {
            entity.HasQueryFilter(q => !q.IsDeleted);

            entity.ToTable("pay_grade");

            entity.HasKey(q => q.PayGradeId);

            entity.Property(q => q.PayGradeId)
                .ValueGeneratedOnAdd()
                .HasColumnOrder(0)
                .HasColumnName("id")
                .IsRequired(true);

            entity.Property(q => q.PayGradeName)
                .HasColumnOrder(1)
                .HasColumnName("name")
                .HasMaxLength(30)
                .IsRequired(true);

            #region "BaseProperties"

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
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired(true);

            entity.Property(q => q.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired(false);

            entity.Property(q => q.DeletedAt)
                 .HasColumnName("deleted_at")
                 .IsRequired(false);

            #endregion

            #region "DefaultData"

            //entity.HasData(new PayGradeDAL
            //{
            //    PayGradeId = 1,
            //    PayGradeName = "Meister"
            //});

            //entity.HasData(new PayGradeDAL
            //{
            //    PayGradeId = 2,
            //    PayGradeName = "Geselle"
            //});

            //entity.HasData(new PayGradeDAL
            //{
            //    PayGradeId = 3,
            //    PayGradeName = "Hilfsarbeiter"
            //});

            //entity.HasData(new PayGradeDAL
            //{
            //    PayGradeId = 4,
            //    PayGradeName = "Feldarbeiter"
            //});

            #endregion

        }
    }
}
