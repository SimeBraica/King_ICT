using DAL.Models;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace API {
    public class Cache {
        private readonly IMemoryCache _memoryCache;

        public Cache(IMemoryCache memoryCache) {
            _memoryCache = memoryCache;
        }

        public void AddToCache(string cacheKey, Product product) {
            if (product == null) return;

            var cacheExpiryOptions = new MemoryCacheEntryOptions {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromSeconds(30)
            };

            _memoryCache.Set(cacheKey, product, cacheExpiryOptions);
        }

        public Product GetFromCache(string cacheKey) {
            _memoryCache.TryGetValue(cacheKey, out Product product);
            return product;
        }
    }
}
