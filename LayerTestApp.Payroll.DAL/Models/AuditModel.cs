using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerTestApp.Payroll.DAL.Models
{
    public abstract class AuditModel
    {
        public DateTime CreatedAt { get; set; }

        public DateTime? LastUpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
