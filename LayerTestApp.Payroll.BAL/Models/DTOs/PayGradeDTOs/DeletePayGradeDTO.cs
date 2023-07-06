namespace LayerTestApp.Payroll.BAL.Models.DTOs.PayGradeDTOs
{
    public class DeletePayGradeDTO
    {
        public int PayGradeId { get; set; }

        public DeletePayGradeDTO(int payGradeId)
        {
            PayGradeId = payGradeId;
        }
    }
}
