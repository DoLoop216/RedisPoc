using Microsoft.Extensions.Caching.StackExchangeRedis;
using StackExchange.Redis;

namespace RedisPOC;

public static class Constants
{
    public static RedisCache MemoryCache = new (new RedisCacheOptions()
    {
        InstanceName = "RedisPOC",
        ConfigurationOptions = new ConfigurationOptions()
        {
            SyncTimeout = 30000,
            EndPoints = new EndPointCollection()
            {
                { "85.90.245.17", 6379 }
            }
        }
    });
}