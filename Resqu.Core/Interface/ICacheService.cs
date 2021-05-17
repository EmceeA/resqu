using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resqu.Core.Interface
{
    public interface ICacheService
    {

        Task<bool> CacheValue(string key, string data, string collection = "default", long expirationInMin = 1440);
        Task<string> GetCachedValue(string key, string collection = "default");
        Task<bool> RemoveCache(string key, string collection = "default");

    }
}
