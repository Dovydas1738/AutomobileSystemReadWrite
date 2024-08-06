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

        [HttpGet("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var allCars = _autoRentService.GetAllCustomers();
                return Ok(allCars);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer(Customer customer)
        {
            try
            {
                _autoRentService.AddNewCustomer(customer);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPatch("UpdateCustomer")]
        public IActionResult UpdateCustomer(Customer customer)
        {
            try
            {

                _autoRentService.RenewCustomer(customer);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("DeleteCustomerById")]
        public IActionResult DeleteCustomerById(int id)
        {
            try
            {
                _autoRentService.DeleteCustomerById(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("GetAllCustomersOrders")]
        public IActionResult GetAllCustomersOrders(int id)
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
