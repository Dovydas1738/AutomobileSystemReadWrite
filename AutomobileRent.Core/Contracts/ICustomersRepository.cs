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
        List<Customer> ReadCustomersDB();
        void WriteCustomerDB(Customer customer);
    }
}
