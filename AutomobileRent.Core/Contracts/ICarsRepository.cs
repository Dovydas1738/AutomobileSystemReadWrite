using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobileRent.Core.Models;

namespace AutomobileRent.Core.Contracts
{
    public interface ICarsRepository
    {
        List<Car> ReadCars();
        void WriteCars(List<Car> cars);
        void WriteOneCar(Car car);
    }
}
