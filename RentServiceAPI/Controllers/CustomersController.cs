using Microsoft.AspNetCore.Mvc;
using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Repositories;
using AutomobileRent.Core.Services;
using AutomobileRent.Core.Models;

namespace RentServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly AutoRentService _autoRentService;

        public CustomersController(AutoRentService autoRentService)
        {
            _autoRentService = autoRentService;
        }

        [HttpGet("GetCustomerById")]
        public async Task<IActionResult> GetWorkerById(int id)
        {
            var customer = await _autoRentService.GetCustomerById(id);
            return Ok(customer);
        }

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var allCars = await _autoRentService.GetAllCustomers();
                return Ok(allCars);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            try
            {
                await _autoRentService.AddNewCustomer(customer);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPatch("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(Customer customer)
        {
            try
            {

                await _autoRentService.RenewCustomer(customer);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("DeleteCustomerById")]
        public async Task<IActionResult> DeleteCustomerById(int id)
        {
            try
            {
                await _autoRentService.DeleteCustomerById(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("GetAllCustomersOrders")]
        public async Task<IActionResult> GetAllCustomersOrders(int id)
        {
            try
            {
                List<RentOrder> allCustomerOrders = new List<RentOrder>();

                foreach (RentOrder a in _autoRentService.GetAllRentOrders())
                {
                    if (id == a.CustomerId)
                    {
                        allCustomerOrders.Add(a);
                    }
                }
                return Ok(allCustomerOrders);
            }
            catch
            {
                return Problem();
            }
        }

    }
}
