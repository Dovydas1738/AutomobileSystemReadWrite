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

        public Electric(string maker, string model, decimal rentPrice, int batteryCapacity, decimal chargeTime) : base(maker, model, rentPrice)
        {
            Maker = maker;
            Model = model;
            RentPrice = rentPrice;
            BatteryCapacity = batteryCapacity;
            ChargeTime = chargeTime;

        }

        public Electric(int id) : base(id)
        {
            Id = id;
        }
        public Electric()
        {

        }

        public override string ToString()
        {
            return $"Id: {Id}; Maker: {Maker}; Model: {Model}; Rent price: {Math.Round(RentPrice,2)} Eur/day; Battery capacity {BatteryCapacity}; Charge time: {ChargeTime} hr.";
        }

    }
}
