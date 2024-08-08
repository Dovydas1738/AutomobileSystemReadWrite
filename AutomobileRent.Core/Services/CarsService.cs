using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Models;
using AutomobileRent.Core.Repositories;

namespace AutomobileRent.Core.Services
{
    public class CarsService : ICarsService
    {
        
        private readonly ICarsRepository _carsRepository;


        public CarsService(ICarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }


        public void ReadFromFile()
        {
            _carsRepository.ReadCars();
        }

        public void WriteToFile(List<Car> cars)
        {
            _carsRepository.WriteCars(cars);
        }

        public async Task<List<Car>> GetAllCars()
        {
            List<Car> allCars = new List<Car>();
            var electric = _carsRepository.ReadAllElectric();
            var combustion = _carsRepository.ReadAllCombustion();
            await Task.WhenAll(electric, combustion);
            allCars.AddRange(electric.Result);
            allCars.AddRange(combustion.Result);

            return allCars.ToList();
        }

        public void AddCar(Car car)
        {
            _carsRepository.WriteOneCar(car);
        }

        public List<Car> SearchByMaker(string maker)
        {
            List<Car> searchResults = new List<Car>();
            List<Electric> elCars = _carsRepository.ReadAllElectric().Result;
            List<Combustion> combCars = _carsRepository.ReadAllCombustion().Result;
            
            
            foreach(Car a in elCars)
            {
                if(a.Maker == maker)
                {
                    searchResults.Add(a);
                }
            }

            foreach (Car a in combCars)
            {
                if (a.Maker == maker)
                {
                    searchResults.Add(a);
                }
            }


            return searchResults;
        }

        public async Task<List<Electric>> ReadAllElectric()
        {
            return await _carsRepository.ReadAllElectric();
        }

        public async Task<List<Combustion>> ReadAllCombustion()
        {
            return await _carsRepository.ReadAllCombustion();
        }

        public async Task WriteOneElectric(Electric electric)
        {
            await _carsRepository.WriteOneElectric(electric);
        }

        public async Task WriteOneCombustion(Combustion combustion)
        {
            await _carsRepository.WriteOneCombustion(combustion);
        }

        public async Task<Electric> GetElectricCarById(int id)
        {
            return await _carsRepository.GetElectricCarById(id);
        }

        public async Task<Combustion> GetCombustionCarById(int id)
        {
            return await _carsRepository.GetCombustionCarById(id);
        }

        public async Task RenewElectric(Electric electric)
        {
            await _carsRepository.RenewElectric(electric);
        }

        public async Task RenewCombustion(Combustion combustion)
        {
            await _carsRepository.RenewCombustion(combustion);
        }

        public async Task DeleteElectricCarById(int id)
        {
            await _carsRepository.DeleteElectricById(id);
        }

        public async Task DeleteCombustionCarById(int id)
        {
            await _carsRepository.DeleteCombustionById(id);
        }


    }
}
