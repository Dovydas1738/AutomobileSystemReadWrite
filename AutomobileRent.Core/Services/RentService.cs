using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobileRent.Core.Repositories;
using AutomobileRent.Core.Services;

namespace AutomobileRent.Core.Services
{
    public class RentService : IRentService
    {
        private readonly IRentOrderRepository _rentOrderRepository;
        
        public RentService(IRentOrderRepository rentOrderRepository)
        {
            _rentOrderRepository = rentOrderRepository;
        }

        List<RentOrder> AllOrders = new List<RentOrder>();



        //public void CreateOrder(RentOrder order)
        //{
        //    AllOrders.Add(order);

        //    Console.WriteLine("Your order was successful!");
        //    Console.WriteLine("Price: " + order.CountRentPrice());
        //    Console.WriteLine("Rental ends: " + order.GetRentEndDate);
        //}

        //public List<RentOrder> GetAllOrders()
        //{
        //    return AllOrders;
        //}

        //public List<RentOrder> GetOrdersByCustomer(Customer customer)
        //{
        //    List<RentOrder> customerOrders = new List<RentOrder>();
        //    foreach (RentOrder order in AllOrders)
        //    {
        //        if (order.Customer == customer)
        //        {
        //            customerOrders.Add(order);
        //        }
        //    }
        //    return customerOrders;
        //}

        public async Task<decimal> GetTotalRentPrice()
        {
            decimal totalPrice = 0;

            foreach (RentOrder order in await _rentOrderRepository.ReadAllRentOrders())
            {
                totalPrice += order.RentPrice;
            }
            return totalPrice;
        }

        public async Task<List<RentOrder>> ReadAllRentOrders()
        {
            return await _rentOrderRepository.ReadAllRentOrders();
        }

        public async Task WriteOneRentOrder(RentOrder rentOrder)
        {
            await _rentOrderRepository.WriteOneRentOrder(rentOrder);
        }

        public async Task<RentOrder> GetRentOrderById(int id)
        {
            return await _rentOrderRepository.GetRentOrderById(id);
        }

        public async Task RenewRentOrder(RentOrder rentOrder)
        {
            await _rentOrderRepository.RenewRentOrder(rentOrder);
        }
        public async Task DeleteRentOrderById(int id)
        {
            await _rentOrderRepository.DeleteRentOrderById(id);
        }
    }
}
