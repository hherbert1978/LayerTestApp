using AutoMapper;
using LayerTestApp.Common.Exceptions;
using LayerTestApp.Payroll.BAL.Models.DTOs.PayGradeDTOs;
using LayerTestApp.Payroll.BAL.ServiceContracts;
using LayerTestApp.Payroll.DAL.Entities;
using LayerTestApp.Payroll.DAL.RepositoryContracts;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace LayerTestApp.Payroll.BAL.Services
{
    public class PayGradeService : IPayGradeService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IPayGradeRepository _payGradeRepository;

        public PayGradeService(IPayGradeRepository payGradeRepository, IMapper mapper, ILogger logger)
        {
            _payGradeRepository = payGradeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<PayGradeResponseDTO>> GetAllAsync(CancellationToken ct = default)
        {
            var payGrades = await _payGradeRepository.GetAllAsync(ct);
            return _mapper.Map<IEnumerable<PayGradeResponseDTO>>(payGrades);
        }

        public async Task<IEnumerable<PayGradeResponseDTO>> GetAllActiveAsync(CancellationToken ct = default)
        {
            var activePayGrades = await _payGradeRepository.GetFilteredAsync(x => x.IsActive, ct);
            return _mapper.Map<IEnumerable<PayGradeResponseDTO>>(activePayGrades);
        }

        public async Task<IEnumerable<PayGradeResponseDTO>> GetAllDeletedAsync(CancellationToken ct = default)
        {
            var deletedPayGrades = await _payGradeRepository.GetAllDeletedAsync(ct);
            return _mapper.Map<IEnumerable<PayGradeResponseDTO>>(deletedPayGrades);
        }

        public async Task<PayGradeResponseDTO> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var payGrade = await _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == id, ct);

            if (payGrade == null)
            {
                _logger.LogError("PayGradeService (GetByIdAsync) - ResourceNotFoundException: No Pay Grade with id = \"{id}\" was found.", id);
                throw new ResourceNotFoundException($"No Pay Grade with id = \"{id}\" was found.");
            }

            return _mapper.Map<PayGradeResponseDTO>(payGrade);
        }

        public async Task<PayGradeResponseDTO> GetByNameAsync(string name, CancellationToken ct = default)
        {
            var payGrade = await _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeName == name, ct);

            if (payGrade == null)
            {
                _logger.LogError("PayGradeService (GetByNameAsync) - ResourceNotFoundException: No Pay Grade with name = \"{name}\" was found.", name);
                throw new ResourceNotFoundException($"No Pay Grade with name = \"{name}\" was found.");
            }

            return _mapper.Map<PayGradeResponseDTO>(payGrade);
        }

        public async Task<bool> NameExistsAsync(string name, CancellationToken ct = default)
        {
            var payGrade = await _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeName == name, ct);
            return payGrade != null;
        }

        public async Task<int> GetIdByNameAsync(string name, CancellationToken ct = default)
        {
            var payGrade = await _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeName == name, ct);

            if (payGrade == null)
            {
                _logger.LogError("PayGradeService (GetIdByNameAsync) - ResourceNotFoundException: No Pay Grade with name = \"{name}\" was found.", name);
                throw new ResourceNotFoundException($"No Pay Grade with name = \"{name}\" was found.");
            }

            return payGrade.PayGradeId;
        }

        public async Task<PayGradeResponseDTO> CreateAsync(CreatePayGradeDTO createPayGradeDTO, CancellationToken ct = default)
        {
            var payGrade = _mapper.Map<PayGrade>(createPayGradeDTO);
            var createdPayGrade = await _payGradeRepository.AddAsync(payGrade, ct);
            return _mapper.Map<PayGradeResponseDTO>(createdPayGrade);
        }

        public async Task<PayGradeResponseDTO> UpdateAsync(UpdatePayGradeDTO updatePayGradeDTO, CancellationToken ct = default)
        {
            var id = updatePayGradeDTO.PayGradeId;
            var payGrade = await _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == id, ct);
            
            if (payGrade == null)
            {
                _logger.LogError("PayGradeService (UpdateAsync) - ResourceNotFoundException: No Pay Grade with id = \"{id}\" was found.", id);
                throw new ResourceNotFoundException($"No Pay Grade with id = \"{id}\" was found.");
            }

            payGrade.PayGradeName = updatePayGradeDTO.PayGradeName;
            payGrade.IsActive = updatePayGradeDTO.IsActive;

            var updatedPayGrade = await _payGradeRepository.UpdateAsync(payGrade, ct);

            return _mapper.Map<PayGradeResponseDTO>(updatedPayGrade);
        }

        public async Task<PayGradeDeleteResponseDTO> DeleteAsync(DeletePayGradeDTO deletePayGradeDTO, CancellationToken ct = default) 
        {
            var id = deletePayGradeDTO.PayGradeId;
            var payGrade = await _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == id, ct);

            if (payGrade == null)
            {
                _logger.LogError("PayGradeService (DeleteAsync) - ResourceNotFoundException: No Pay Grade with id = \"{id}\" was found.", id);
                throw new ResourceNotFoundException($"No Pay Grade with id = \"{id}\" was found.");
            }

            var isDeleted = await _payGradeRepository.DeleteAsync(payGrade, ct);

            return new PayGradeDeleteResponseDTO { PayGradeId = id, IsDeleted = isDeleted };
        }

    }
}
