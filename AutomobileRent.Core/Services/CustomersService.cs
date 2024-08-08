using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobileRent.Core.Models;
using AutomobileRent.Core.Services;
using AutomobileRent.Core.Repositories;
using AutomobileRent.Core.Contracts;

namespace AutomobileRent.Core.Services
{
    public class CustomersService : ICustomersService
    {

        private readonly ICustomersRepository _customersRepository;
        private readonly IMongoDbCacheRepository _mongoCache;

        public CustomersService(ICustomersRepository customersRepository, IMongoDbCacheRepository mongoCache)
        {
            _customersRepository = customersRepository;
            _mongoCache = mongoCache;
        }

        public void AddCustomer(Customer customer)
        {
            _customersRepository.WriteOneCustomer(customer);
        }

        public async Task DeleteCustomerById(int id)
        {
            await _customersRepository.DeleteCustomerById(id);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customersRepository.ReadCustomers();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            Customer result;
            if ((result = await _mongoCache.GetCustomerById(id)) != null)
            {
                return result;
            }
            result = await _customersRepository.GetCustomerById(id);
            await _mongoCache.AddCustomer(result);
            return result;
        }

        public async Task<List<Customer>> ReadCustomersDB()
        {
            return await _customersRepository.ReadCustomersDB();
        }

        public void ReadFromFile()
        {
            _customersRepository.ReadCustomers();
        }

        public async Task RenewCustomer(Customer customer)
        {
            await _customersRepository.RenewCustomer(customer);
        }

        public List<Customer> SearchByNameSurname(string name, string surname)
        {
            List<Customer> customerSearchResult = new List<Customer>();
            List<Customer> customers = _customersRepository.ReadCustomersDB().Result;
            foreach (Customer b in customers)
            {
                if (b.Name == name && b.Surname == surname)
                {
                    customerSearchResult.Add(b);
                }
            }
            return customerSearchResult;
        }

        public async Task WriteCustomerDB(Customer customer)
        {
            await _customersRepository.WriteCustomerDB(customer);
        }

        public void WriteToFile(List<Customer> customers)
        {
            _customersRepository.WriteCustomers(customers);
        }
    }
}
