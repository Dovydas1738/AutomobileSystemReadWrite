using AutomobileRent.Core.Models;
using AutomobileRent.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoRent.FrontEnd.Pages
{
    public class CombustionModel : PageModel
    {
        private readonly AutoRentService _autoRentService;
        public List<Combustion> CombustionCars;

        public CombustionModel(AutoRentService autoRentService)
        {
            _autoRentService = autoRentService;
        }

        public void OnGet()
        {
            CombustionCars = _autoRentService.GetAllCombustion();
        }
    }
}
