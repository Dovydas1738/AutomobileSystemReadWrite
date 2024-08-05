using AutomobileRent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileRent.Core.Contracts
{
    public interface IWorkerService
    {
        List<Worker> ReadWorkersDB();
        Worker GetWorkerById(int id);
        decimal GetWorkerBaseSalary(int workerId);
        List<RentOrder> GetWorkerCompletedOrders(int workerId);
        void PayOutSalary(int workerId, decimal workerSalary);
        void AddWorker(Worker worker);
        void RenewWorkerData(Worker worker);
        void DeleteWorkerById(int workerId);
    }
}
