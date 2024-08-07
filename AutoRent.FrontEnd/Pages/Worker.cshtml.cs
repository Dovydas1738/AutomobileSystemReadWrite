using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutomobileRent.Core.Models;
using AutomobileRent.Core.Services;

namespace AutoRent.FrontEnd.Pages
{
    public class WorkerModel : PageModel
    {
        private readonly WorkerService _workerService;
        public List<Worker> Workers;

        public WorkerModel(WorkerService workerService)
        {
            _workerService = workerService;
        }
        
        public void OnGet()
        {
            Workers = _workerService.ReadWorkersDB();
        }
    }
}
