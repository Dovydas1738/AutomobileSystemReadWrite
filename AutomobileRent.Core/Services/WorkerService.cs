using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileRent.Core.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerDBRepository _workerRepository;
        private readonly IMongoDbCacheRepository _mongoCache;

        public WorkerService(IWorkerDBRepository workerRepository, IMongoDbCacheRepository mongoCache)
        {
            _workerRepository = workerRepository;
            _mongoCache = mongoCache;
        }

        public async Task AddWorker(Worker worker)
        {
            await _workerRepository.AddWorker(worker);
        }

        public async Task AddWorkersBaseSalary(Worker worker, decimal salary)
        {
            await _workerRepository.AddWorkersBaseSalary(worker,salary);
        }

        public async Task DeleteWorkerById(int workerId)
        {
            await _workerRepository.DeleteWorkerById(workerId);
        }

        public async Task<decimal> GetWorkerBaseSalary(int workerId)
        {
            return await _workerRepository.GetWorkerBaseSalary(workerId);
        }

        public async Task<Worker> GetWorkerById(int id)
        {
            Worker result;
            if ((result = await _mongoCache.GetWorkerById(id)) != null)
            {
                return result;
            }
            result = await _workerRepository.GetWorkerById(id);
            await _mongoCache.AddWorker(result);
            return result;
        }

        public Worker GetWorkerByNameSurname(string name, string surname)
        {
            return _workerRepository.GetWorkerByNameSurname(name, surname);
        }

        public async Task<List<RentOrder>> GetWorkerCompletedOrders(int workerId)
        {
            return await _workerRepository.GetWorkerCompletedOrders(workerId);
        }

        public async Task PayOutSalary(int workerId, decimal workerSalary)
        {
            await _workerRepository.PayOutSalary(workerId, workerSalary);
        }

        public async Task<List<Worker>> ReadWorkersDB()
        {
            return await _workerRepository.ReadWorkersDB();
        }

        public async Task RenewWorkerData(Worker worker)
        {
            await _workerRepository.RenewWorkerData(worker);
        }

        public async Task UpdateWorkerBaseSalary(Worker worker, decimal salary)
        {
            await _workerRepository.UpdateWorkerBaseSalary(worker, salary);
        }
    }
}
