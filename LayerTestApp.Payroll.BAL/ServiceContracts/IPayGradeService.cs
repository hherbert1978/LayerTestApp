using LayerTestApp.Payroll.BAL.DTOs.PayGradeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerTestApp.Payroll.BAL.ServiceContracts
{
    public interface IPayGradeService
    {
        Task<List<ViewPayGradeDTO>> GetAllAsync();

        Task<ViewPayGradeDTO> CreateAsync(CreatePayGradeDTO createPayGradeDTO);
    }
}
