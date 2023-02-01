using AutoMapper;
using LayerTestApp.Payroll.BAL.Entities;
using LayerTestApp.Payroll.DAL.Models;

namespace LayerTestApp.Payroll.BAL.Mapper
{
    public class EntityToBALProfile : Profile
    {
        public EntityToBALProfile()
        {
            CreateMap<PayGrade, PayGradeBAL>();
                
        }

    }
}
