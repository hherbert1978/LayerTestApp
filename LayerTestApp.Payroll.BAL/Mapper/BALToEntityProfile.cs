using AutoMapper;
using LayerTestApp.Payroll.BAL.Entities;
using LayerTestApp.Payroll.DAL.Models;

namespace LayerTestApp.Payroll.BAL.Mapper
{
    public class BALToEntityProfile : Profile
    {
        public BALToEntityProfile()
        {
            CreateMap<PayGradeBAL, PayGrade>()
                .ForMember(m => m.IsDeleted, opt => opt.MapFrom(src => src.IsDeleting));
        }
    }
}
