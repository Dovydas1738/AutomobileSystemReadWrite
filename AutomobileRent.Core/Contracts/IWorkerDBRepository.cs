using AutomobileRent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileRent.Core.Contracts
{
    public interface IWorkerDBRepository
    {
        Task<List<Worker>> ReadWorkersDB();
        Task<Worker> GetWorkerById(int id);
        Task<decimal> GetWorkerBaseSalary(int workerId);
        Task<List<RentOrder>> GetWorkerCompletedOrders(int workerId);
        Task PayOutSalary(int workerId, decimal workerSalary);
        Task AddWorker(Worker worker);
        Task RenewWorkerData(Worker worker);
        Task DeleteWorkerById(int workerId);
        Task AddWorkersBaseSalary(Worker worker, decimal salary);
        Worker GetWorkerByNameSurname(string name, string surname);
        Task UpdateWorkerBaseSalary(Worker worker, decimal salary);
    }
}
