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
        private readonly ICarsRepository _carsRepository;
        private readonly ICustomersRepository _customersRepository;

        List<RentOrder> AllOrders = new List<RentOrder>();



        public void CreateOrder(RentOrder order)
        {
            AllOrders.Add(order);

            //Console.WriteLine("Your order was successful!");
            //Console.WriteLine("Price: " + order.CountRentPrice());
            //Console.WriteLine("Rental ends: " + order.GetRentEndDate);
        }

        public List<RentOrder> GetAllOrders()
        {
            return AllOrders;
        }

        public List<RentOrder> GetOrdersByCustomer(Customer customer)
        {
            List<RentOrder> customerOrders = new List<RentOrder>();
            foreach (RentOrder order in AllOrders)
            {
                if (order.Customer == customer)
                {
                    customerOrders.Add(order);
                }
            }
            return customerOrders;
        }

        public decimal GetTotalRentPrice()
        {
            decimal totalPrice = 0;

            foreach (RentOrder order in AllOrders)
            {
                totalPrice += order.Car.RentPrice * order.RentDuration;
            }
            return totalPrice;
        }
    }
}
