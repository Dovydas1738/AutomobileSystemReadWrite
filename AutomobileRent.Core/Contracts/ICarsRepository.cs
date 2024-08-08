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
        Task<List<Electric>> ReadAllElectric();
        Task<List<Combustion>> ReadAllCombustion();
        Task WriteOneElectric(Electric electric);
        Task WriteOneCombustion(Combustion combustion);
        Task<Electric> GetElectricCarById(int id);
        Task<Combustion> GetCombustionCarById(int id);
        Task RenewElectric(Electric electric);
        Task RenewCombustion(Combustion combustion);
        Task DeleteElectricById(int id);
        Task DeleteCombustionById(int id);

    }
}
