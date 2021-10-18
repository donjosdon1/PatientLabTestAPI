using PatientLabTestAPI.Common;

namespace PatientLabTestAPI.Controllers
{
    public interface IApiBase<T1> : IBase<T1> where T1 : class
    {

    }
}
