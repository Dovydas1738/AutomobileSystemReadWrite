using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileRent.Core.Models
{
    public class RentOrder
    {
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public DateTime RentStart { get; set; }
        public int RentDuration { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public int CarId { get; set; }
        public string Type { get; set; }

        public int CustomerId { get; set; }
        public decimal RentPrice { get; set; }



        public RentOrder(Customer customer, Car car, string type, DateTime rentStart, int rentDuration)
        {
            Customer = customer;
            Car = car;
            Type = type;
            RentStart = rentStart;
            RentDuration = rentDuration;
            RentPrice = CountRentPrice();
        }

        //public RentOrder(Customer customer, Car car, string type, DateTime rentStart, int rentDuration)
        //{
        //    Customer = customer;
        //    Car = car;
        //    Type = type;
        //    RentStart = rentStart;
        //    RentDuration = rentDuration;
        //    RentPrice = CountRentPrice();
        //}


        public RentOrder() { }

        //public RentOrder(int customerId, int carId, string type, DateTime rentStart, int rentDuration)
        //{
        //    CustomerId = customerId;
        //    CarId = carId;
        //    Type = type;
        //    RentStart = rentStart;
        //    RentDuration = rentDuration;
        //    RentPrice = CountRentPrice();
        //}

        public decimal CountRentPrice()
        {
            return Car.RentPrice * RentDuration;
        }

        public DateTime GetRentEndDate()
        {
            return RentStart.AddDays(RentDuration);
        }
        public override string ToString()
        {
            return $"Customer id: {CustomerId}, Car id: {CarId}, rent start: {RentStart}, duration: {RentDuration}";
        }


    }
}
