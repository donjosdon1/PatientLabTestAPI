using PatientLabTestAPI.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Controllers
{
    public interface IApiBase<TRequest, TResponse> 
    {
        Task<IEnumerable<TResponse>> GetAllData();
        Task<TResponse> GetDataByKey(long key);
        Task<TResponse> CreateRecord(TRequest record);
        Task<TResponse> UpdateRecord(TRequest record);
        Task<Message> Delete(long key);

    }
}
