namespace LayerTestApp.Payroll.BAL.Models.DTOs.PayGradeDTOs
{
    public class CreatePayGradeDTO
    {
        public string PayGradeName { get; set; }

        public CreatePayGradeDTO(string payGradeName)
        {
            PayGradeName = payGradeName;
        }        
    }
}
