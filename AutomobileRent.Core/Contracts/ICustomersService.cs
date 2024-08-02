using AutomobileRent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileRent.Core.Contracts
{
    public interface ICustomersService
    {
        void ReadFromFile();
        void WriteToFile(List<Customer> customers);
        List<Customer> GetAllCustomers();
        void AddCustomer(Customer customer);
        List<Customer> SearchByNameSurname(string name, string surname);
        List<Customer> ReadCustomersDB();
        void WriteCustomerDB(Customer customer);
        Customer GetCustomerById(int id);
        void RenewCustomer(Customer customer);
    }
}
