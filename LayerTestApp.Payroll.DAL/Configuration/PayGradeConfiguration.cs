using LayerTestApp.Payroll.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LayerTestApp.Payroll.DAL.Configuration
{
    public class PayGradeConfiguration : BaseModelConfiguration<PayGrade>
    {
        public override void Configure(EntityTypeBuilder<PayGrade> entity)
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
                .HasMaxLength(50)
                .IsRequired(true);

            base.Configure(entity);

            #region "DefaultData"

            //entity.HasData(new PayGrade
            //{
            //    PayGradeId = 1,
            //    PayGradeName = "Meister"
            //});

            //entity.HasData(new PayGrade
            //{
            //    PayGradeId = 2,
            //    PayGradeName = "Geselle"
            //});

            //entity.HasData(new PayGrade
            //{
            //    PayGradeId = 3,
            //    PayGradeName = "Hilfsarbeiter"
            //});

            //entity.HasData(new PayGrade
            //{
            //    PayGradeId = 4,
            //    PayGradeName = "Feldarbeiter"
            //});

            #endregion

        }
    }
}
