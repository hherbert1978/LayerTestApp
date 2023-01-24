using AutoMapper;
using LayerTestApp.Payroll.DAL.Entities;
using LayerTestApp.Payroll.DAL.Models;

namespace LayerTestApp.Payroll.DAL.Mapper
{
    public class EntityToDALProfile : Profile
    {
        public EntityToDALProfile()
        {
            CreateMap<PayGrade, PayGradeDAL>()
                .ForMember(m => m.IsDeleted, opt => opt.MapFrom(src => src.IsDeleting));
        }

    }
}
