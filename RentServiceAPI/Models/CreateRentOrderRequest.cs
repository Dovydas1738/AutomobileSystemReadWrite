using AutomobileRent.Core.Models;

namespace RentServiceAPI.Models
{
    public class CreateRentOrderRequest
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public DateTime RentStart { get; set; }
        public int RentDuration { get; set; }
        //public string CustomerName { get; set; }
        //public string CustomerSurname { get; set; }
        public string Type { get; set; }
        //public decimal RentPrice { get; set; }
        public int WorkerId { get; set; }

    }
}
