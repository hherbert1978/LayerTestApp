namespace LayerTestApp.Payroll.BAL.Entities
{
    public class PayGradeBAL
    {
        public int? PayGradeId { get; set; }

        public string PayGradeName { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleting { get; set; } = false;

        public PayGradeBAL() { }

        public PayGradeBAL(int payGradeId)
        {
            PayGradeId = payGradeId;
        }

        public PayGradeBAL(string payGradeName)
        {
            PayGradeName = payGradeName;
        }

        public PayGradeBAL(string payGradeName, bool isActive)
        {
            PayGradeName = payGradeName;
            IsActive = isActive;
        }

        public PayGradeBAL(int payGradeId, bool isActive)
        {
            PayGradeId = payGradeId;
            IsActive = isActive;
        }

        public PayGradeBAL(int payGradeId, string payGradeName, bool isActive)
        {
            PayGradeId = payGradeId;
            PayGradeName = payGradeName;
            IsActive = isActive;
        }

    }
}
