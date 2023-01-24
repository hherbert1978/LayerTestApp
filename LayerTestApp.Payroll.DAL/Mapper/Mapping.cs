using AutoMapper;

namespace LayerTestApp.Payroll.DAL.Mapper
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> lazyMapper = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DALToEntityProfile>();
                cfg.AddProfile<EntityToDALProfile>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => lazyMapper.Value;

    }
}
