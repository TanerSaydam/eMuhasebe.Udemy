using eMuhasebeServer.Application.Services;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text.Json;

namespace eMuhasebeServer.Infrastructure.Services;
internal sealed class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public T? Get<T>(string key)
    {
        var value = _database.StringGet(key);
        if (value.HasValue)
        {
            var result = JsonConvert.DeserializeObject<T?>(value.ToString());
            return result;
        }

        return default(T?);
    }

    public bool Remove(string key)
    {
        return _database.KeyDelete(key);
    }

    public void RemoveAll()
    {
        List<string> keys = new()
        {
            "cashRegisters",
            "banks",
            "invoices",
            "products",
            "customers"
        };
        foreach (var key in keys)
        {
            _database.KeyDelete(key);
        }
    }

    public void Set<T>(string key, T value, TimeSpan? expiry = null)
    {
        var serializedValue = JsonConvert.SerializeObject(value);
        _database.StringSet(key, serializedValue, expiry);
    }
}
