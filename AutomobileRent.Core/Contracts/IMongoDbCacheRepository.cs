using AutomobileRent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileRent.Core.Contracts
{
    public interface IMongoDbCacheRepository
    {
        Task AddWorker(Worker worker);
        Task<Worker> GetWorkerById(int id);
        Task AddCustomer(Customer customer);
        Task<Customer> GetCustomerById(int customerId);
    }
}
