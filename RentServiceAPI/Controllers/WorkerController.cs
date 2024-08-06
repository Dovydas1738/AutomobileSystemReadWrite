using AutomobileRent.Core.Models;
using AutomobileRent.Core.Services;
using Microsoft.AspNetCore.Mvc;
using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Repositories;

namespace RentServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly AutoRentService _autoRentService;
        private readonly IWorkerService _workerService;

        public WorkerController(AutoRentService autoRentService, IWorkerService workerService)
        {
            _autoRentService = autoRentService;
            _workerService = workerService;
        }

        [HttpGet("GetAllWorkers")]
        public IActionResult GetAllWorkers()
        {
            try
            {
                var allWorkers = _workerService.ReadWorkersDB();
                return Ok(allWorkers);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost("AddWorker")]
        public IActionResult AddWorker(Worker worker, decimal newWorkerSalary)
        {
            try
            {
                _workerService.AddWorker(worker);

                Worker createdWorker = _workerService.GetWorkerByNameSurname(worker.Name, worker.Surname);

                _workerService.AddWorkersBaseSalary(createdWorker, newWorkerSalary);

                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPatch("UpdateWorker")]
        public IActionResult UpdateWorker(Worker worker, decimal updatedWorkerSalary)
        {
            try
            {
                _workerService.RenewWorkerData(worker);

                Worker chosenWorker = _workerService.GetWorkerByNameSurname(worker.Name, worker.Surname);

                _workerService.UpdateWorkerBaseSalary(chosenWorker, updatedWorkerSalary);

                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("DeleteWorkerById")]
        public IActionResult DeleteWorkerById(int id)
        {
            try
            {
                _workerService.DeleteWorkerById(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("PayOutSalary")]
        public IActionResult PayOutSalary(int id)
        {
            Worker chosenWorker = _workerService.GetWorkerById(id);

            int orderCount = 0;

            foreach (RentOrder a in _workerService.GetWorkerCompletedOrders(id))
            {
                orderCount++;
            }

            decimal workerSalary = chosenWorker.WorkerSalary(_workerService.GetWorkerBaseSalary(id), orderCount);
            _workerService.PayOutSalary(id, workerSalary);

            return Ok(workerSalary);

        }
    }
}
