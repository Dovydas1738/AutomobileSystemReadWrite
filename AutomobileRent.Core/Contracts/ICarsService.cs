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
        Task<List<Car>> GetAllCars();
        void AddCar(Car car);
        List<Car> SearchByMaker(string maker);
        Task<List<Electric>> ReadAllElectric();
        Task<List<Combustion>> ReadAllCombustion();
        Task WriteOneElectric(Electric electric);
        Task WriteOneCombustion(Combustion combustion);
        Task<Electric> GetElectricCarById(int id);
        Task<Combustion> GetCombustionCarById(int id);
        Task RenewElectric(Electric electric);
        Task RenewCombustion(Combustion combustion);
        Task DeleteElectricCarById(int id);
        Task DeleteCombustionCarById(int id);
    }
}
