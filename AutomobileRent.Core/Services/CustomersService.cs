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

        public Customer GetCustomerById(int id)
        {
            return _customersRepository.GetCustomerById(id);
        }

        public List<Customer> ReadCustomersDB()
        {
            return _customersRepository.ReadCustomersDB();
        }

        public void ReadFromFile()
        {
            _customersRepository.ReadCustomers();
        }

        public void RenewCustomer(Customer customer)
        {
            _customersRepository.RenewCustomer(customer);
        }

        public List<Customer> SearchByNameSurname(string name, string surname)
        {
            List<Customer> customerSearchResult = new List<Customer>();
            List<Customer> customers = _customersRepository.ReadCustomersDB();
            foreach (Customer b in customers)
            {
                if (b.Name == name && b.Surname == surname)
                {
                    customerSearchResult.Add(b);
                }
            }
            return customerSearchResult;
        }

        public void WriteCustomerDB(Customer customer)
        {
            _customersRepository.WriteCustomerDB(customer);
        }

        public void WriteToFile(List<Customer> customers)
        {
            _customersRepository.WriteCustomers(customers);
        }
    }
}
