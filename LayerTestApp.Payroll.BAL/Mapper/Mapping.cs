using AutoMapper;

namespace LayerTestApp.Payroll.BAL.Mapper
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> lazyMapper = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PayGradeProfile>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => lazyMapper.Value;

    }
}
