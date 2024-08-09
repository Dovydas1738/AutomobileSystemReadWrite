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

        [HttpGet("GetElectricById")]
        public async Task<IActionResult> GetElectricById(int id)
        {
            var electric = await _autoRentService.GetElectricCarById(id);
            return Ok(electric);
        }

        [HttpGet("GetCombustionById")]
        public async Task<IActionResult> GetCombustionById(int id)
        {
            var combustion = await _autoRentService.GetCombustionCarById(id);
            return Ok(combustion);
        }


        [HttpGet("GetAllCars")]
        public async Task<IActionResult> GetAllCars()
        {
            var allCars = await _autoRentService.GetAllCars();
            return Ok(allCars);
        }

        [HttpGet("GetAllElectricCars")]
        public async Task<IActionResult> Index()
        {
            var allCars = await _autoRentService.GetAllElectric();
            return Ok(allCars);
        }

        [HttpGet("GetAllCombustionCars")]
        public async Task<IActionResult> GetAllCombustion()
        {
            var allCars = await _autoRentService.GetAllCombustion();
            return Ok(allCars);
        }

        [HttpPost("AddElectric")]
        public async Task<IActionResult> AddElectric(Electric ev)
        {
            try
            {
                await _autoRentService.AddNewElectric(ev);
                return Ok();

            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost("AddCombustion")]
        public async Task<IActionResult> AddCombustion(Combustion combustion)
        {
            try
            {
                await _autoRentService.AddNewCombustion(combustion);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPatch("UpdateElectric")]
        public async Task<IActionResult> UpdateElectric(Electric ev)
        {
            try
            {

                await _autoRentService.RenewElectric(ev);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPatch("UpdateCombustion")]
        public async Task<IActionResult> UpdateCombustion(Combustion combustion)
        {
            try
            {

                await _autoRentService.RenewCombustion(combustion);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("DeleteElectricById")]
        public async Task<IActionResult> DeleteElectricById(int id)
        {
            try
            {
                await _autoRentService.DeleteElectricCarById(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("DeleteCombustionById")]
        public async Task<IActionResult> DeleteCombustionById(int id)
        {
            try
            {
                await _autoRentService.DeleteCombustionCarById(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }


    }
}
