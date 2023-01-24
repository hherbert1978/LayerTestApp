namespace LayerTestApp.Payroll.DAL.Models
{
    public class BaseModel
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

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
