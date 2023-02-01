using LayerTestApp.Payroll.DAL.Contracts;
using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Models;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerTestApp.Payroll.DAL.Repositories
{
    public class PayGradeRepository : BaseRepository<PayGrade>, IPayGradeRepository
    {
        public PayGradeRepository(ILogger<PayGradeRepository> logger, LTAPayrollDbContext context) : base(logger, context) { }
    }
}
