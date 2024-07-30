using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileRent.Core.Models
{
    public class Electric : Car
    {
        public int BatteryCapacity { get; set; }
        public decimal ChargeTime { get; set; }


        public Electric(int id, string maker, string model, decimal rentPrice, int batteryCapacity, decimal chargeTime) : base (id,maker,model,rentPrice)
        {
            Id = id;
            Maker = maker;
            Model = model;
            RentPrice = rentPrice;
            BatteryCapacity = batteryCapacity;
            ChargeTime = chargeTime;

        }

        public override string ToString()
        {
            return $"Id: {Id}; Maker: {Maker}; Model: {Model}; Rent price: {RentPrice} Eur/day; Battery capacity {BatteryCapacity}; Charge time: {ChargeTime} hr.";
        }

    }
}
