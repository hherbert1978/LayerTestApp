namespace LayerTestApp.Payroll.BAL.Models.DTOs.PayGradeDTOs
{
    public class PayGradeResponseDTO : BaseResponseDTO
    {
        public string PayGradeName { get; set; }

        public bool IsActive { get; set; }
    }
}
