using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobileRent.Core.Services;
using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Models;

namespace AutomobileRent.Core.Services
{
    public class AutoRentService
    {
        private readonly ICarsService _carsService;
        private readonly ICustomersService _customersService;
        private readonly IRentService _rentService;

        public AutoRentService(ICarsService carsService, ICustomersService customersService, IRentService rentService)
        {
            _carsService = carsService;
            _customersService = customersService;
            _rentService = rentService;
        }

        public List<Car> GetCars()
        {
            return _carsService.GetAllCars();
        }

        public void AddNewCar(Car car)
        {
            _carsService.AddCar(car);
        }

    }
}
