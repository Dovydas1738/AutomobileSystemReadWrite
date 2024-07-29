using System;
using System.Collections.Generic;
using System.Linq;
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


        public RentOrder(Customer customer, Car car, DateTime rentStart, int rentDuration)
        {
            Customer = customer;
            Car = car;
            RentStart = rentStart;
            RentDuration = rentDuration;
        }

        public decimal CountRentPrice()
        {
            return Car.RentPrice * RentDuration;
        }

        public DateTime GetRentEndDate()
        {
            return RentStart.AddDays(RentDuration);
        }

    }
}
