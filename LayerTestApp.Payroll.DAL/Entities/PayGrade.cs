namespace LayerTestApp.Payroll.DAL.Entities
{
    public class PayGrade
    {
        public int? PayGradeId { get; set; }

        public string PayGradeName { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleting { get; set; } = false;

        public PayGrade() { }

        public PayGrade(int payGradeId)
        {
            PayGradeId = payGradeId;
        }

        public PayGrade(string payGradeName)
        {
            PayGradeName = payGradeName;
        }

        public PayGrade(string payGradeName, bool isActive)
        {
            PayGradeName = payGradeName;
            IsActive = isActive;
        }

        public PayGrade(int payGradeId, bool isActive)
        {
            PayGradeId = payGradeId;
            IsActive = isActive;
        }

        public PayGrade(int payGradeId, string payGradeName, bool isActive)
        {
            PayGradeId = payGradeId;
            PayGradeName = payGradeName;
            IsActive = isActive;
        }

    }
}
