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
        List<Worker> ReadWorkersDB();
        Worker GetWorkerById(int id);
        decimal GetWorkerBaseSalary(int workerId);
        List<RentOrder> GetWorkerCompletedOrders(int workerId);
        void PayOutSalary(int workerId, decimal workerSalary);
    }
}
