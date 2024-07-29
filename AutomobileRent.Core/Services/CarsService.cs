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
            List<Car> cars = _carsRepository.ReadCars();
            foreach(Car a in cars)
            {
                if(a.Maker == maker)
                {
                    searchResults.Add(a);
                }
            }
            return searchResults;
        }
    }
}
