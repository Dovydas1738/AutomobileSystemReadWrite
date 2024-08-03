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
        List<Electric> ReadAllElectric();
        List<Combustion> ReadAllCombustion();
        void WriteOneElectric(Electric electric);
        void WriteOneCombustion(Combustion combustion);
        Electric GetElectricCarById(int id);
        Combustion GetCombustionCarById(int id);
        void RenewElectric(Electric electric);
        void RenewCombustion(Combustion combustion);
        void DeleteElectricById(int id);
        void DeleteCombustionById(int id);
    }
}
