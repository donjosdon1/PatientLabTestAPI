using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Repository
{
    public interface IBaseRepo<T1, T2> where T1 : class
    {
        Task<IEnumerable<T1>> GetAllData();
        Task<T1> GetDataByKey(T2 key);
        Task<T1> CreateRecord(T1 record);
        Task Delete(T2 key);
        Task<IEnumerable<T1>> GetFilteredData(DateTime startDate, DateTime endDate);
    }
}
