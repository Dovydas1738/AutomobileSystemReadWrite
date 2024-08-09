using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileRent.Core.Models
{
    public class Combustion : Car
    {
        public decimal FuelConsumption { get; set; }

        public Combustion(int id, string maker, string model, decimal rentPrice, decimal fuelConsumption) : base(id, maker, model, rentPrice)
        {
            Id = id;
            Maker = maker;
            Model = model;
            RentPrice = rentPrice;
            FuelConsumption = fuelConsumption;
        }

        public Combustion(string maker, string model, decimal rentPrice, decimal fuelConsumption) : base(maker, model, rentPrice)
        {
            Maker = maker;
            Model = model;
            RentPrice = rentPrice;
            FuelConsumption = fuelConsumption;
        }

        public Combustion(int id) : base(id)
        {
            Id = id;
        }

        public Combustion() { }

        public override string ToString()
        {
            return $"Id: {Id}; Maker: {Maker}; Model: {Model}; Rent price: {Math.Round(RentPrice,2)} Eur/day; Fuel consumption: {Math.Round(FuelConsumption,1)} l/100km";
        }


    }
}
