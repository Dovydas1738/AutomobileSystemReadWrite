using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutomobileRent.Core.Models;
using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Services;

namespace AutoRent.FrontEnd.Pages
{
    public class ElectricModel : PageModel
    {
        private readonly AutoRentService _autoRentService;
        public List<Electric> ElectricCars;

        public ElectricModel(AutoRentService autoRentService)
        {
            _autoRentService = autoRentService;
        }

        public void OnGet()
        {
            ElectricCars = _autoRentService.GetAllElectric();
        }
    }
}
