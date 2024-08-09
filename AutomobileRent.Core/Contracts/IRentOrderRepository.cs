using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using AutomobileRent.Core.Models;

namespace AutomobileRent.Core.Contracts
{
    public interface IRentOrderRepository
    {
        Task<List<RentOrder>> ReadAllRentOrders();
        Task WriteOneRentOrder(RentOrder rentOrder);
        Task RenewRentOrder(RentOrder rentOrder);
        Task<RentOrder> GetRentOrderById(int id);
        Task DeleteRentOrderById(int id);
    }
}
