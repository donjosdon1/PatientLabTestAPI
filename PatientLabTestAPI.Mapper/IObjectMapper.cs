using System.Collections.Generic;

namespace PatientLabTestAPI.Mapper
{
    public interface IObjectMapper
    {
        TDestination MapObject<TSource, TDestination>(TSource source);
        IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source);
    }
}
