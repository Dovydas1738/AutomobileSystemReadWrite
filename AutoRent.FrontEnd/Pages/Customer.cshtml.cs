using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutomobileRent.Core.Models;
using AutomobileRent.Core.Services;

namespace AutoRent.FrontEnd.Pages
{
    public class CustomerModel : PageModel
    {
		private readonly AutoRentService _autoRentService;
		public List<Customer> Customers;

		public CustomerModel(AutoRentService autoRentService)
		{
			_autoRentService = autoRentService;
		}

		public void OnGet()
        {
			Customers = _autoRentService.GetAllCustomers();
        }
    }
}
