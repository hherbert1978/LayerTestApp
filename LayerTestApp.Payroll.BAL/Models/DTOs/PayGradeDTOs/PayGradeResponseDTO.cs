namespace LayerTestApp.Payroll.BAL.Models.DTOs.PayGradeDTOs
{
    public class PayGradeResponseDTO : BaseResponsePayGradeDTO
    {
        public string PayGradeName { get; set; }

        public bool IsActive { get; set; }
    }
}
