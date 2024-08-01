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

        public List<Car> GetAllCars()
        {
            return _carsRepository.ReadCars();
        }

        public void AddCar(Car car)
        {
            _carsRepository.WriteOneCar(car);
        }

        public List<Car> SearchByMaker(string maker)
        {
            List<Car> searchResults = new List<Car>();
            List<Electric> elCars = _carsRepository.ReadAllElectric();
            List<Combustion> combCars = _carsRepository.ReadAllCombustion();
            
            
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

        public List<Electric> ReadAllElectric()
        {
            return _carsRepository.ReadAllElectric();
        }

        public List<Combustion> ReadAllCombustion()
        {
            return _carsRepository.ReadAllCombustion();
        }

        public void WriteOneElectric(Electric electric)
        {
            _carsRepository.WriteOneElectric(electric);
        }

        public void WriteOneCombustion(Combustion combustion)
        {
            _carsRepository.WriteOneCombustion(combustion);
        }
    }
}
