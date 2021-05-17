using Microsoft.Extensions.Configuration;
using Resqu.Core.Constants;
using Resqu.Core.Interface;
using Serilog;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Utility
{
    public class RedisCacheService : ICacheService
    {
        private StackExchange.Redis.IDatabase redis;
        private readonly IConfiguration _config;
        public RedisCacheService(IConfiguration config)
        {
            _config = config;
            var redisServer = _config.GetSection("Redis:Server").Value;
            try
            {
                var connectionMultiplexer = ConnectionMultiplexer.Connect(redisServer);
                redis = connectionMultiplexer.GetDatabase(0);
            }
            catch (Exception ex)
            {
                Log.Fatal($"Error : {ex.Message}");
            }
        }
        public async Task<bool> CacheValue(string key, string data, string collection = "default", long expirationInMin = 1440)
        {
            var resp = await redis.HashSetAsync(ConstantValue.CACHE_KEY_APP_DEFAULT + collection, key, data);

            await redis.KeyExpireAsync(ConstantValue.CACHE_KEY_APP_DEFAULT + collection, TimeSpan.FromMinutes(expirationInMin));
            return resp;
        }

        public async Task<string> GetCachedValue(string key, string collection = "default")
        {
            return await redis.HashGetAsync(ConstantValue.CACHE_KEY_APP_DEFAULT + collection, key);
        }

        public async Task<bool> RemoveCache(string key, string collection = "default")
        {
            return await redis.HashDeleteAsync(ConstantValue.CACHE_KEY_APP_DEFAULT + collection, key);
        }
    }
}
