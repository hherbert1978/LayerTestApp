using AutoMapper;

namespace LayerTestApp.Payroll.BAL.Mapper
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> lazyMapper = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BALToEntityProfile>();
                cfg.AddProfile<EntityToBALProfile>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => lazyMapper.Value;

    }
}
