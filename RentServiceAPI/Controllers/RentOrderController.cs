using Microsoft.AspNetCore.Mvc;
using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Repositories;
using AutomobileRent.Core.Services;
using AutomobileRent.Core.Models;

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
        public IActionResult GetAllRentOrders()
        {
            try
            {
                var allRentOrders = _autoRentService.GetAllRentOrders();
                return Ok(allRentOrders);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost("CreateRentOrder")]
        public IActionResult CreateRentOrder(RentOrder rentOrder)
        {
            try
            {
                _autoRentService.AddOneRentOrder(rentOrder);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPatch("UpdateRentOrder")]
        public IActionResult UpdateRentOrder(RentOrder rentOrder)
        {
            try
            {

                _autoRentService.RenewRentOrder(rentOrder);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("DeleteRentOrderById")]
        public IActionResult DeleteRentOrderById(int id)
        {
            try
            {
                _autoRentService.DeleteRentOrderById(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("GetTotalRentPrice")]
        public IActionResult GetTotalRentPrice()
        {
            try
            {
                decimal totalPrice = Math.Round(_rentService.GetTotalRentPrice(), 2);
                return Ok(totalPrice);
            }
            catch
            {
                return Problem();
            }
        }

    }
}
