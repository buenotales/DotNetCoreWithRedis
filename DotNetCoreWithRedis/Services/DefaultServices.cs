using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreWithRedis.Services
{
    public class DefaultServices
    {
        private readonly IDistributedCache _cache;
        private const string _keyCache = "cacheDotNetCore";
        private const int _timeOutCache = 10;

        public DefaultServices(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetCache(string value)
        {
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(_timeOutCache));

            byte[] encodedValue = Encoding.UTF8.GetBytes(value);

            await _cache.SetAsync(_keyCache, encodedValue, options);
        }

        public async Task<string> GetCache()
        {
            var encodedValue = Encoding.UTF8.GetString(await _cache.GetAsync(_keyCache) ?? Encoding.UTF8.GetBytes(string.Empty));

            return encodedValue;
        }

        public async Task RemoveCache()
        {
            await _cache.RemoveAsync(_keyCache);
        }

        public async Task RefreshCache()
        {
            await _cache.RefreshAsync(_keyCache);
        }
    }
}
