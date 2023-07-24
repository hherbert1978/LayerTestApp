using System.Text.Json.Serialization;

namespace LayerTestApp.Payroll.BAL.Models.DTOs.PayGradeDTOs
{
    public class CreatePayGradeDTO
    {
        public string PayGradeName { get; set; }

        public bool IsActive { get; set; }

        public CreatePayGradeDTO(string payGradeName)
        {
            PayGradeName = payGradeName;
            IsActive = true;
        }

        [JsonConstructor]
        public CreatePayGradeDTO(string payGradeName, bool isActive)
        {
            PayGradeName = payGradeName;
            IsActive = isActive;    
        }
    }
}
