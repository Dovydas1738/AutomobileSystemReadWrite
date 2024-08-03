using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Models;

namespace AutomobileRent.Core.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly string _filePath;

        public CustomersRepository(string customerFilePath)
        {
        _filePath = customerFilePath; 
        }

        public void DeleteCustomerById(int id)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Customer> ReadCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (StreamReader customerReader = new StreamReader(this._filePath))
            {
                while (!customerReader.EndOfStream)
                {
                    string line = customerReader.ReadLine();
                    string[] values = line.Split(",");
                    customers.Add(new Customer(values[0], values[1], DateTime.Parse(values[2])));
                }
                return customers;
            }

        }

        public List<Customer> ReadCustomersDB()
        {
            throw new NotImplementedException();
        }

        public void RenewCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void WriteCustomerDB(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void WriteCustomers(List<Customer> customers)
        {
            foreach (Customer customer in customers)
            {
                using (StreamWriter customerWriter = new StreamWriter(this._filePath, true))
                {
                    customerWriter.WriteLine($"{customer.Name},{customer.Surname},{customer.BirthDate}");
                }

            }
        }

        public void WriteOneCustomer(Customer customer)
        {
            using (StreamWriter customerWriter = new StreamWriter(this._filePath, true))
            {
                customerWriter.WriteLine($"{customer.Name},{customer.Surname},{customer.BirthDate}");
            }
        }
    }
}
