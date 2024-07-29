using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileRent.Core.Models
{
    internal class Combustion : Car
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
    }
}
