using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobileRent.Core.Models;

namespace AutomobileRent.Core.Contracts
{
    public interface ICustomersRepository
    {
        List<Customer> ReadCustomers();
        void WriteCustomers(List<Customer> customers);
        void WriteOneCustomer(Customer customer);
        Task<List<Customer>> ReadCustomersDB();
        Task WriteCustomerDB(Customer customer);
        Task<Customer> GetCustomerById(int id);
        Task RenewCustomer(Customer customer);
        Task DeleteCustomerById(int id);

    }
}
