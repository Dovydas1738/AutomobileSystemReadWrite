using AutomobileRent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileRent.Core.Contracts
{
     public interface IRentService
     {
        //void CreateOrder(RentOrder order);
        //List<RentOrder> GetAllOrders();
        Task<decimal> GetTotalRentPrice();
        //List<RentOrder> GetOrdersByCustomer(Customer customer);
        Task<List<RentOrder>> ReadAllRentOrders();
        Task WriteOneRentOrder(RentOrder rentOrder);
        Task<RentOrder> GetRentOrderById(int id);
        Task RenewRentOrder(RentOrder rentOrder);
        Task DeleteRentOrderById(int id);

     }
}
