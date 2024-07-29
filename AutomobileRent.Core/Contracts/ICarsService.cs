using AutomobileRent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileRent.Core.Contracts
{
    public interface ICarsService
    {
        void ReadFromFile();
        void WriteToFile(List<Car> cars);
        List<Car> GetAllCars();
        void AddCar(Car car);
        List<Car> SearchByMaker(string maker);


    }
}
