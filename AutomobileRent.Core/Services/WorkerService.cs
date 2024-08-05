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

        public WorkerService(IWorkerDBRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public decimal GetWorkerBaseSalary(int workerId)
        {
            return _workerRepository.GetWorkerBaseSalary(workerId);
        }

        public Worker GetWorkerById(int id)
        {
            return _workerRepository.GetWorkerById(id);
        }

        public List<RentOrder> GetWorkerCompletedOrders(int workerId)
        {
            return _workerRepository.GetWorkerCompletedOrders(workerId);
        }

        public void PayOutSalary(int workerId, decimal workerSalary)
        {
            _workerRepository.PayOutSalary(workerId, workerSalary);
        }

        public List<Worker> ReadWorkersDB()
        {
            return _workerRepository.ReadWorkersDB();
        }
    }
}
