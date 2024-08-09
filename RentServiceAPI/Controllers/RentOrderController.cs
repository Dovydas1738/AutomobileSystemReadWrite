using Microsoft.AspNetCore.Mvc;
using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Repositories;
using AutomobileRent.Core.Services;
using AutomobileRent.Core.Models;
using RentServiceAPI.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace RentServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentOrderController : ControllerBase
    {
        private readonly AutoRentService _autoRentService;
        private readonly IRentService _rentService;

        public RentOrderController(AutoRentService autoRentService, IRentService rentService)
        {
            _autoRentService = autoRentService;
            _rentService = rentService;
        }

        [HttpGet("GetAllRentOrders")]
        public async Task<IActionResult> GetAllRentOrders()
        {
            try
            {
                var allRentOrders = await _autoRentService.GetAllRentOrders();
                return Ok(allRentOrders);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost("CreateRentOrder")]
        public async Task<IActionResult> CreateRentOrder(CreateRentOrderRequest rentOrder)
        {
            try
            {
                RentOrder order = new RentOrder();

                if (rentOrder.Type == "Electric")
                {
                    order.Customer = new Customer(rentOrder.CustomerId);
                    order.Car = new Electric(rentOrder.CarId);
                    order.RentStart = rentOrder.RentStart;
                    order.RentDuration = rentOrder.RentDuration;
                    order.Type = rentOrder.Type;
                    order.RentPrice = _autoRentService.GetElectricCarById(rentOrder.CarId).Result.RentPrice * order.RentDuration;
                    order.Worker = new Worker(rentOrder.WorkerId);
                }
                else
                {
                    order.Customer = new Customer(rentOrder.CustomerId);
                    order.Car = new Combustion(rentOrder.CarId);
                    order.RentStart = rentOrder.RentStart;
                    order.RentDuration = rentOrder.RentDuration;
                    order.Type = rentOrder.Type;
                    order.RentPrice = _autoRentService.GetCombustionCarById(rentOrder.CarId).Result.RentPrice * order.RentDuration;
                    order.Worker = new Worker(rentOrder.WorkerId);

                }


                await _autoRentService.AddOneRentOrder(order);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPatch("UpdateRentOrder")]
        public async Task<IActionResult> UpdateRentOrder(CreateRentOrderRequest rentOrder)
        {
            try
            {
                RentOrder order = new RentOrder();

                if (rentOrder.Type == "Electric")
                {
                    order.Customer = new Customer(rentOrder.CustomerId);
                    order.Car = new Electric(rentOrder.CarId);
                    order.RentStart = rentOrder.RentStart;
                    order.RentDuration = rentOrder.RentDuration;
                    order.Type = rentOrder.Type;
                    order.RentPrice = _autoRentService.GetElectricCarById(rentOrder.CarId).Result.RentPrice * order.RentDuration;
                    order.Worker = new Worker(rentOrder.WorkerId);
                    order.Id = rentOrder.Id;
                }
                else
                {
                    order.Customer = new Customer(rentOrder.CustomerId);
                    order.Car = new Combustion(rentOrder.CarId);
                    order.RentStart = rentOrder.RentStart;
                    order.RentDuration = rentOrder.RentDuration;
                    order.Type = rentOrder.Type;
                    order.RentPrice = _autoRentService.GetCombustionCarById(rentOrder.CarId).Result.RentPrice * order.RentDuration;
                    order.Worker = new Worker(rentOrder.WorkerId);
                    order.Id = rentOrder.Id;

                }


                await _autoRentService.RenewRentOrder(order);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("DeleteRentOrderById")]
        public async Task<IActionResult> DeleteRentOrderById(int id)
        {
            try
            {
                await _autoRentService.DeleteRentOrderById(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("GetTotalRentPrice")]
        public async Task<IActionResult> GetTotalRentPrice()
        {
            try
            {
                decimal totalPrice = Math.Round(_rentService.GetTotalRentPrice().Result, 2);
                return Ok(totalPrice);
            }
            catch
            {
                return Problem();
            }
        }

    }
}
