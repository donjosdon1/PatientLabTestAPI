using System.Collections.Generic;

namespace PatientLabTestAPI.Mapper
{
    public interface IObjectMapper//<in TSource, out TDestination>
    {
        TDestination MapObject<TSource, TDestination>(TSource source);
        IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source);
    }
}
