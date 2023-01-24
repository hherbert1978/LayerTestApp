using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerTestApp.Payroll.DAL.Models
{
    public class PayGradeDAL : BaseModel
    {
        public int PayGradeId { get; set; }

        public string PayGradeName { get; set; }
    }
}
