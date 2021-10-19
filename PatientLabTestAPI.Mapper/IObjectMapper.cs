using System.Threading.Tasks;

namespace PatientLabTestAPI.Mapper
{
    public interface IObjectMapper<in TSource, out TDestination>
    {
        TDestination MapObject(TSource source);
    }
}
