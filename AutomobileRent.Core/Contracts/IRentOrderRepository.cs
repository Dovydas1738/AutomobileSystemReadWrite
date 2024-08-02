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
        List<RentOrder> ReadAllRentOrders();
        void WriteOneRentOrder(RentOrder rentOrder);
        void RenewRentOrder(RentOrder rentOrder);
        RentOrder GetRentOrderById(int id);
    }
}
