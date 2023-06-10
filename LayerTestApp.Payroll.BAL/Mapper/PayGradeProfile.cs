using AutoMapper;
using LayerTestApp.Payroll.BAL.Models.DTOs.PayGradeDTOs;
using LayerTestApp.Payroll.DAL.Entities;

namespace LayerTestApp.Payroll.BAL.Mapper
{
    public class PayGradeProfile : Profile
    {
        public PayGradeProfile()
        {
            //// DAL (PayGrade) to BAL (PayGradeBAL) and reverse   
            //CreateMap<PayGrade, PayGradeBAL>();

            //CreateMap<PayGradeBAL, PayGrade>();

            // BAL to DTO and reverse    
            CreateMap<CreatePayGradeDTO, PayGrade>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<PayGrade, PayGradeResponseDTO>();
        }
    }
}
