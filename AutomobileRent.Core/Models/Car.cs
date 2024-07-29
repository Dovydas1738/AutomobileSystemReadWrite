using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileRent.Core.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
        public decimal RentPrice { get; set; }

        public Car(int id, string maker, string model, decimal rentPrice)
        {
            Id = id;
            Maker = maker;
            Model = model;
            RentPrice = rentPrice;
        }

        public string GetInfo()
        {
            return $"Id: {Id}; Maker: {Maker}; Model: {Model}; Rent price: {RentPrice} Eur/day.";
        }


    }
}
