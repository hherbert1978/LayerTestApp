using LayerTestApp.Payroll.BAL.Models.DTOs.PayGradeDTOs;

namespace LayerTestApp.Payroll.BAL.ServiceContracts
{
    public interface IPayGradeService
    {
        Task<IEnumerable<PayGradeResponseDTO>> GetAllAsync(CancellationToken ct = default);

        Task<IEnumerable<PayGradeResponseDTO>> GetAllActiveAsync(CancellationToken ct = default);

        Task<IEnumerable<PayGradeResponseDTO>> GetAllDeletedAsync(CancellationToken ct = default);

        Task<PayGradeResponseDTO> GetByIdAsync(int id, CancellationToken ct = default);

        Task<PayGradeResponseDTO> GetByNameAsync(string name, CancellationToken ct = default);

        Task<bool> IdExistsAsync(int id, CancellationToken ct = default);

        Task<bool> NameExistsAsync(string name, CancellationToken ct = default);

        Task<int> GetIdByNameAsync(string name, CancellationToken ct = default);

        Task<PayGradeResponseDTO> CreateAsync(CreatePayGradeDTO createPayGradeDTO, CancellationToken ct = default);

        Task<PayGradeResponseDTO> UpdateAsync(UpdatePayGradeDTO updatePayGradeDTO, CancellationToken ct = default);

        Task<PayGradeDeleteResponseDTO> DeleteAsync(DeletePayGradeDTO deletePayGradeDTO, CancellationToken ct = default);
    }
}
