using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Common
{
    public interface IBase<T1> where T1 : class
    {
        Task<IEnumerable<T1>> GetAllData();
        Task<T1> GetDataByKey(long key);
        Task<T1> CreateRecord(T1 record);
        Task<T1> UpdateRecord(T1 record);
        Task<Message> Delete(long key);
    }
}
