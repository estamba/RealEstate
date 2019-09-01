using Microsoft.Extensions.Caching.Memory;
using RealEstate.Core.Interfaces.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Services.Utilities
{
    public class CacheManager : ICacheManager
    {
        IMemoryCache cache;
        public CacheManager(IMemoryCache cache)
        {
            this.cache = cache;
        }
        public T Get<T>(string key)
        {

            if (cache.TryGetValue(key, out object cacheEntry))
                return (T)cacheEntry;
            return default(T);
        }

        public void Store(string key, object value, DateTime expirationDate)
        {
            var expirationDuration = expirationDate - DateTime.Now;
            MemoryCacheEntryOptions memoryCacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(expirationDuration);
            cache.Set(key, value, memoryCacheEntryOptions);
        }
    }
}
