namespace LayerTestApp.Payroll.BAL.Models.DTOs.PayGradeDTOs
{
    public class UpdatePayGradeDTO
    {
        public int PayGradeId { get; set; }

        public string PayGradeName { get; set; }
        
        public bool IsActive { get; set; }

        public UpdatePayGradeDTO(int payGradeId, string payGradeName, bool isActive)
        {
            PayGradeId = payGradeId;
            PayGradeName = payGradeName;
            IsActive = isActive;
        }
    }
}
