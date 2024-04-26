using eMuhasebeServer.Application.Services;
using Microsoft.Extensions.Caching.Memory;

namespace eMuhasebeServer.Infrastructure.Services;
internal sealed class MemoryCacheService(
    IMemoryCache cache) : ICacheService
{  
    public T? Get<T>(string key)
    {
        var result = cache.TryGetValue<T>(key, out var value);

        return value;
    }

    public bool Remove(string key)
    {
        cache.Remove(key);

        return true;
    }

    public void Set<T>(string key, T value, TimeSpan? expiry = null)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromHours(1),
        };

        cache.Set<T>(key, value, cacheEntryOptions);
    }
}
