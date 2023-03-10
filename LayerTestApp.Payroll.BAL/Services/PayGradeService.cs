using AutoMapper;
using LayerTestApp.Payroll.BAL.DTOs.PayGradeDTOs;
using LayerTestApp.Payroll.BAL.Entities;
using LayerTestApp.Payroll.BAL.ServiceContracts;
using LayerTestApp.Payroll.DAL.Models;
using LayerTestApp.Payroll.DAL.RepositoryContracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerTestApp.Payroll.BAL.Services
{
    public class PayGradeService : IPayGradeService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IPayGradeRepository _payGradeRepository;

        public PayGradeService(IPayGradeRepository payGradeRepository, IMapper mapper, ILogger logger) {
            _payGradeRepository = payGradeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ViewPayGradeDTO>> GetAllAsync(CancellationToken ct = default)
        {
            var payGrades = await _payGradeRepository.GetAllAsync(ct);
            var payGradeBALs = _mapper.Map<List<PayGradeBAL>>(payGrades);
            var viewPayGradeDTO = _mapper.Map<List<ViewPayGradeDTO>>(payGradeBALs);

            return viewPayGradeDTO;
        }

        public async Task<ViewPayGradeDTO> CreateAsync(CreatePayGradeDTO createPayGradeDTO)
        {
            var payGradeBAL = new PayGradeBAL(createPayGradeDTO.PayGradeName);
            var payGrade = _mapper.Map<PayGrade>(payGradeBAL);

            var newPayGrade = await _payGradeRepository.AddAsync(payGrade);

            return new ViewPayGradeDTO();
        }
    }
}
