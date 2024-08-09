using MongoDB.Driver;
using AutomobileRent.Core.Models;
using AutomobileRent.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileRent.Core.Repositories
{
    public class MongoDbCacheRepository : IMongoDbCacheRepository
    {
        private IMongoCollection<Worker> _workersCache;
        private IMongoCollection<Customer> _customersCache;
        private IMongoCollection<Electric> _electricCache;
        private IMongoCollection<Combustion> _combustionCache;

        public MongoDbCacheRepository(IMongoClient mongoClient)
        {
            _workersCache = mongoClient.GetDatabase("workers").GetCollection<Worker>("workers_cache");
            _customersCache = mongoClient.GetDatabase("customers").GetCollection<Customer>("customers_cache");
            _electricCache = mongoClient.GetDatabase("electricCars").GetCollection<Electric>("electricCars_cache");
            _combustionCache = mongoClient.GetDatabase("combustionCars").GetCollection<Combustion>("combustionCars_cache");
        }
        
        //LIKO cachinti rent orderius ir tuos concernus sutaisyt kas apacioj
        // GET ALL padaryt kad jeigu neranda deda i cache, kaip su get id?????????? ensure/check consistency funkcija su whiletrue, lygini mongo ir db count'a, jei nelygu trini. mongo get countdocuments turi but long
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

        public async Task UpdateWorker(Worker worker)
        {
            await _workersCache.ReplaceOneAsync(w => w.Id == worker.Id, worker);
        }

        public async Task AddCustomer(Customer customer)
        {
            await _customersCache.InsertOneAsync(customer); //NEPRIDEDA ID, REIKIA GET BY ID KAD PRIDETU
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

        public async Task UpdateCustomer(Customer customer)
        {
            await _customersCache.ReplaceOneAsync(w => w.CustomerId == customer.CustomerId, customer);
        }

        public async Task AddElectric(Electric electric)
        {
            await _electricCache.InsertOneAsync(electric);
        }

        public async Task<Electric> GetElectricById(int electricId)
        {
            try
            {
                return (await _electricCache.FindAsync<Electric>(x => x.Id == electricId)).First();
            }
            catch
            {
                return null;
            }
        }

        public async Task UpdateElectric(Electric electric)
        {
            await _electricCache.ReplaceOneAsync(e => e.Id == electric.Id, electric);
        }


        public async Task AddCombustion(Combustion combustion)
        {
            await _combustionCache.InsertOneAsync(combustion);
        }

        public async Task<Combustion> GetCombustionById(int combustionId)
        {
            try
            {
                return (await _combustionCache.FindAsync<Combustion>(x => x.Id == combustionId)).First();
            }
            catch
            {
                return null;
            }
        }

        public async Task UpdateCombustion(Combustion combustion)
        {
            await _combustionCache.ReplaceOneAsync(e => e.Id == combustion.Id, combustion);
        }


        public async Task DropCaches()
        {
            var dropCustomers = _customersCache.Database.DropCollectionAsync("customers_cache");
            var dropWorkers = _workersCache.Database.DropCollectionAsync("workers_cache");
            var dropElectric = _electricCache.Database.DropCollectionAsync("electricCars_cache");
            var dropCombustion = _combustionCache.Database.DropCollectionAsync("combustionCars_cache");

            await Task.WhenAll(dropCustomers, dropWorkers, dropCombustion, dropElectric);
        }
    }
}
