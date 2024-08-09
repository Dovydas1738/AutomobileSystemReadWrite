using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileRent.Core.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMongoDbCacheRepository _cacheRepository;

        public CacheService (IMongoDbCacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;

        }

        public async Task DeleteCaches()
        {
            while (true)
            {
                Console.WriteLine("Cache clear in 1 minute");
                await Task.Delay(180000);
                await _cacheRepository.DropCaches();
                Console.WriteLine("Cache cleared.");
            }
        }
    }
}
