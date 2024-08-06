using Microsoft.AspNetCore.Mvc;
using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Repositories;
using AutomobileRent.Core.Services;
using AutomobileRent.Core.Models;


namespace RentServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly AutoRentService _autoRentService;

        public CarsController(AutoRentService autoRentService)
        {
            _autoRentService = autoRentService;
        }

        [HttpGet("GetAllElectricCars")]
        public IActionResult Index()
        {
            var allCars = _autoRentService.GetAllElectric();
            return Ok(allCars);
        }

        [HttpGet("GetAllCombustionCars")]
        public IActionResult GetAllCombustion()
        {
            var allCars = _autoRentService.GetAllCombustion();
            return Ok(allCars);
        }

        [HttpPost("AddElectric")]
        public IActionResult AddElectric(Electric ev)
        {
            try
            {
                _autoRentService.AddNewElectric(ev);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost("AddCombustion")]
        public IActionResult AddCombustion(Combustion combustion)
        {
            try
            {
                _autoRentService.AddNewCombustion(combustion);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPatch("UpdateElectric")]
        public IActionResult UpdateElectric(Electric ev)
        {
            try
            {

                _autoRentService.RenewElectric(ev);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPatch("UpdateCombustion")]
        public IActionResult UpdateCombustion(Combustion combustion)
        {
            try
            {

                _autoRentService.RenewCombustion(combustion);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("DeleteElectricById")]
        public IActionResult DeleteElectricById(int id)
        {
            try
            {
                _autoRentService.DeleteElectricCarById(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("DeleteCombustionById")]
        public IActionResult DeleteCombustionById(int id)
        {
            try
            {
                _autoRentService.DeleteCombustionCarById(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }


    }
}
