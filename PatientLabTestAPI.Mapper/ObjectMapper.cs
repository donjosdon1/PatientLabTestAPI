using AutoMapper;

namespace PatientLabTestAPI.Mapper
{
    public class ObjectMapper<TSource, TDestination> : IObjectMapper<TSource, TDestination> where TSource: class
    {
        public TDestination MapObject(TSource source)
        {
            //Initialize the mapper
            var mapper = new AutoMapper.Mapper(new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>()));
            return mapper.Map<TSource, TDestination>(source);
        }
    }
}
