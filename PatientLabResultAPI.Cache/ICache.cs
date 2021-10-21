using System.Collections.Generic;

namespace PatientLabResultAPI.Cache
{
    public interface ICache<T>
    {
        void SetCache(T data, string key);
        T GetCache(string key);
        void Clear(string key);
    }
}
