using AutoMapper;
using System.Collections.Generic;

namespace PatientLabTestAPI.Mapper
{
    public class ObjectMapper : IObjectMapper
    {
        public TDestination MapObject<TSource, TDestination>(TSource source)
        {
            //Initialize the mapper
            var mapper = new AutoMapper.Mapper(new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>()));
            return mapper.Map<TSource, TDestination>(source);
        }
        public IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source)
        {
            var mapper = new AutoMapper.Mapper(new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>()));
            return mapper.Map<IEnumerable<TDestination>>(source);
        }
    }
}
