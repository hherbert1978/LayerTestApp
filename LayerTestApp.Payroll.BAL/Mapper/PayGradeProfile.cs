using AutoMapper;
using LayerTestApp.Payroll.BAL.Entities;
using LayerTestApp.Payroll.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerTestApp.Payroll.BAL.Mapper
{
    public class PayGradeProfile : Profile
    {
        public PayGradeProfile() 
        {
            CreateMap<PayGrade, PayGradeBAL>();
            CreateMap<PayGradeBAL, PayGrade>();
            //.ForMember(m => m.IsDeleted, opt => opt.MapFrom(src => src.IsDeleting));
        }
    }
}
