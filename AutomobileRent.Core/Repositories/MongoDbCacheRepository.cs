using MongoDB.Driver;
using AutomobileRent.Core.Models;
using AutomobileRent.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace AutomobileRent.Core.Repositories
{
    public class MongoDbCacheRepository : IMongoDbCacheRepository
    {
        private IMongoCollection<Worker> _workersCache;
        private IMongoCollection<Customer> _customersCache;

        public MongoDbCacheRepository(IMongoClient mongoClient)
        {
            _workersCache = mongoClient.GetDatabase("workers").GetCollection<Worker>("workers_cache");
            _customersCache = mongoClient.GetDatabase("customers").GetCollection<Customer>("customers_cache");
        }

        public async Task AddWorker(Worker worker)
        {
           await _workersCache.InsertOneAsync(worker);
        }

        public async Task<Worker> GetWorkerById(int id)
        {
            try
            {
                return (await _workersCache.FindAsync<Worker>(x => x.Id == id)).First();
            }
            catch
            {
                return null;
            }
        }

        public async Task AddCustomer(Customer customer)
        {
            await _customersCache.InsertOneAsync(customer);
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            try
            {
                return (await _customersCache.FindAsync<Customer>(x => x.CustomerId == customerId)).First();
            }
            catch
            {
                return null;
            }
        }
    }
}
