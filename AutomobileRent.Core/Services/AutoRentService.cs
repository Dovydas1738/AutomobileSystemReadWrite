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

        public List<Electric> GetAllElectric()
        {
            return _carsService.ReadAllElectric();
        }

        public List<Combustion> GetAllCombustion()
        {
            return _carsService.ReadAllCombustion();
        }

        public void AddNewElectric(Electric electric)
        {
            _carsService.WriteOneElectric(electric);
        }

        public void AddNewCombustion(Combustion combustion)
        {
            _carsService.WriteOneCombustion(combustion);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customersService.ReadCustomersDB();
        }

        public void AddNewCustomer(Customer customer)
        {
            _customersService.WriteCustomerDB(customer);
        }

        public List<RentOrder> GetAllRentOrders()
        {
            return _rentService.ReadAllRentOrders();
        }

        public void AddOneRentOrder(RentOrder rentOrder)
        {
            _rentService.WriteOneRentOrder(rentOrder);
        }

    }
}
