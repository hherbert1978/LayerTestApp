using AutoMapper;
using LayerTestApp.Payroll.DAL.Entities;
using LayerTestApp.Payroll.DAL.Models;

namespace LayerTestApp.Payroll.DAL.Mapper
{
    public class DALToEntityProfile : Profile
    {
        public DALToEntityProfile()
        {
            CreateMap<PayGradeDAL, PayGrade>();
        }
    }
}
