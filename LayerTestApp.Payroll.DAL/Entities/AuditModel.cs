namespace LayerTestApp.Payroll.DAL.Entities
{
    public abstract class AuditModel
    {
        public DateTime CreatedAt { get; set; }

        public DateTime? LastUpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
