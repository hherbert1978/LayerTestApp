namespace LayerTestApp.Payroll.DAL.Models
{
    public abstract class BaseModel : AuditModel
    {
        private bool? _isActive;
        public bool IsActive
        {
            get => _isActive ?? true;
            set => _isActive = value;
        }

        private bool? _isDeleted;
        public bool IsDeleted
        {
            get => _isDeleted ?? true;
            set => _isDeleted = value;
        }

    }
}
