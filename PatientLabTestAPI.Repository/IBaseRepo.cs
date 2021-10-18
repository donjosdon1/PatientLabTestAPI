using PatientLabTestAPI.Common;

namespace PatientLabTestAPI.Repository
{
    public interface IBaseRepo<T1> : IBase<T1> where T1 : class
    {
        
    }
}
