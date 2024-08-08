using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobileRent.Core.Services;
using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Models;
using AutomobileRent.Core.Repositories;

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

        public async Task<List<Car>> GetAllCars()
        {
            return await _carsService.GetAllCars();
        }

        public void AddNewCar(Car car)
        {
            _carsService.AddCar(car);
        }

        public async Task<List<Electric>> GetAllElectric()
        {
            return await _carsService.ReadAllElectric();
        }

        public async Task<List<Combustion>> GetAllCombustion()
        {
            return await _carsService.ReadAllCombustion();
        }

        public async Task AddNewElectric(Electric electric)
        {
            await _carsService.WriteOneElectric(electric);
        }

        public async Task AddNewCombustion(Combustion combustion)
        {
            await _carsService.WriteOneCombustion(combustion);
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _customersService.ReadCustomersDB();
        }

        public async Task AddNewCustomer(Customer customer)
        {
            await _customersService.WriteCustomerDB(customer);
        }

        public List<RentOrder> GetAllRentOrders()
        {
            return _rentService.ReadAllRentOrders();
        }

        public void AddOneRentOrder(RentOrder rentOrder)
        {
            _rentService.WriteOneRentOrder(rentOrder);
        }

        public async Task<Car> GetElectricCarById(int id)
        {
           return await _carsService.GetElectricCarById(id);
        }

        public async Task<Car> GetCombustionCarById(int id)
        {
            return await _carsService.GetCombustionCarById(id);
        }

        public async Task RenewElectric(Electric electric)
        {
            await _carsService.RenewElectric(electric);
        }

        public async Task RenewCombustion(Combustion combustion)
        {
            await _carsService.RenewCombustion(combustion);
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _customersService.GetCustomerById(id);
        }

        public async Task RenewCustomer(Customer customer)
        {
            await _customersService.RenewCustomer(customer);
        }

        public RentOrder GetOrderById(int id)
        {
            return _rentService.GetRentOrderById(id);
        }

        public void RenewRentOrder(RentOrder rentOrder)
        {
            _rentService.RenewRentOrder(rentOrder);
        }

        public async Task DeleteCustomerById(int id)
        {
            await _customersService.DeleteCustomerById(id);
        }

        public async Task DeleteElectricCarById(int id)
        {
            await _carsService.DeleteElectricCarById(id);
        }

        public async Task DeleteCombustionCarById(int id)
        {
            await _carsService.DeleteCombustionCarById(id);
        }

        public void DeleteRentOrderById(int id)
        {
            _rentService.DeleteRentOrderById(id);
        }

    }
}
