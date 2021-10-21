using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace PatientLabResultAPI.Cache
{
    public class Cache<T> : ICache<T>
    {
        private readonly IMemoryCache cache;
        public Cache(IMemoryCache memoryCache) => cache = memoryCache;
        public T GetCache(string key)
        {
            if (cache.TryGetValue<T>(key, out T data))
            {
                return data;
            }
            else
            {
                return default;
            }
        }        

        public void SetCache(T data, string key)
        {
            var options = new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(60) };
            cache.Set(key, data, options);
        }
        
        public void Clear(string key)
        {
            if (cache.TryGetValue<T>(key, out _))
            {
                cache.Remove(key);
            }
        }
    }
}
