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
        private readonly IMongoDbCacheRepository _mongoDbCacheRepository;


        public CarsService(ICarsRepository carsRepository, IMongoDbCacheRepository mongoDbCacheRepository)
        {
            _carsRepository = carsRepository;
            _mongoDbCacheRepository = mongoDbCacheRepository;
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
            var electricSql = _carsRepository.WriteOneElectric(electric);
            var electricMongo = _mongoDbCacheRepository.AddElectric(electric);
            await Task.WhenAll(electricSql, electricMongo);
        }

        public async Task WriteOneCombustion(Combustion combustion)
        {
            var combustionSql = _carsRepository.WriteOneCombustion(combustion);
            var combustionMongo = _mongoDbCacheRepository.AddCombustion(combustion);
            await Task.WhenAll(combustionSql, combustionMongo);
        }

        public async Task<Electric> GetElectricCarById(int id)
        {
            Electric result;
            if ((result = await _mongoDbCacheRepository.GetElectricById(id)) != null)
            {
                return result;
            }
            result = await _carsRepository.GetElectricCarById(id);
            await _mongoDbCacheRepository.AddElectric(result);
            return result;
        }

        public async Task<Combustion> GetCombustionCarById(int id)
        {
            Combustion result;
            if ((result = await _mongoDbCacheRepository.GetCombustionById(id)) != null)
            {
                return result;
            }
            result = await _carsRepository.GetCombustionCarById(id);
            await _mongoDbCacheRepository.AddCombustion(result);
            return result;

        }

        public async Task RenewElectric(Electric electric)
        {
            var electricSql = _carsRepository.RenewElectric(electric);
            var electricMongo = _mongoDbCacheRepository.UpdateElectric(electric);

            await Task.WhenAll(electricSql, electricMongo);
        }

        public async Task RenewCombustion(Combustion combustion)
        {
            var combustionSql = _carsRepository.RenewCombustion(combustion);
            var combustionMongo = _mongoDbCacheRepository.UpdateCombustion(combustion);

            await Task.WhenAll(combustionSql, combustionMongo);
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
