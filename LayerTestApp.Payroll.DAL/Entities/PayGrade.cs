namespace LayerTestApp.Payroll.DAL.Entities
{
    public class PayGrade
    {
        public int? PayGradeId { get; set; }

        public string PayGradeName { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleting { get; set; }

        public PayGrade() { }

        public PayGrade(string payGradeName)
        {
            PayGradeName = payGradeName;
            IsActive = true;
            IsDeleting = false;
        }

        public PayGrade(int payGradeId, string payGradeName, bool isActive)
        {
            PayGradeId = payGradeId;
            PayGradeName = payGradeName;
            IsActive = isActive;
            IsDeleting = false;
        }

        public PayGrade(int payGradeId, string payGradeName, bool isActive, bool isDeleting)
        {
            PayGradeId = payGradeId;
            PayGradeName = payGradeName;
            IsActive = isDeleting ? false : isActive;
            IsDeleting = isDeleting;
        }
    }
}
