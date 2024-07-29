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

        public CustomersService(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public void AddCustomer(Customer customer)
        {
            _customersRepository.WriteOneCustomer(customer);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customersRepository.ReadCustomers();
        }

        public void ReadFromFile()
        {
            _customersRepository.ReadCustomers();
        }

        public List<Customer> SearchByNameSurname(string name, string surname)
        {
            List<Customer> customerSearchResult = new List<Customer>();
            List<Customer> customers = _customersRepository.ReadCustomers();
            foreach (Customer b in customers)
            {
                if (b.Name == name && b.Surname == surname)
                {
                    customerSearchResult.Add(b);
                }
            }
            return customerSearchResult;
        }

        public void WriteToFile(List<Customer> customers)
        {
            _customersRepository.WriteCustomers(customers);
        }
    }
}
