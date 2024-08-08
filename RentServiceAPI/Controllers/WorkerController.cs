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

        [HttpGet("GetWorkerById")]
        public async Task<IActionResult> GetWorkerById(int id)
        {
            var worker = await _workerService.GetWorkerById(id);
            return Ok(worker);
        }

        [HttpGet("GetAllWorkers")]
        public async Task<IActionResult> GetAllWorkers()
        {
            try
            {
                var allWorkers = await _workerService.ReadWorkersDB();
                return Ok(allWorkers);
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost("AddWorker")]
        public async Task<IActionResult> AddWorker(Worker worker, decimal newWorkerSalary)
        {
            try
            {
                await _workerService.AddWorker(worker);

                Worker createdWorker = _workerService.GetWorkerByNameSurname(worker.Name, worker.Surname);

                await _workerService.AddWorkersBaseSalary(createdWorker, newWorkerSalary);

                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPatch("UpdateWorker")]
        public async Task<IActionResult> UpdateWorker(Worker worker, decimal updatedWorkerSalary)
        {
            try
            {
                await _workerService.RenewWorkerData(worker);

                Worker chosenWorker = _workerService.GetWorkerByNameSurname(worker.Name, worker.Surname);

                await _workerService.UpdateWorkerBaseSalary(chosenWorker, updatedWorkerSalary);

                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpDelete("DeleteWorkerById")]
        public async Task<IActionResult> DeleteWorkerById(int id)
        {
            try
            {
                await _workerService.DeleteWorkerById(id);
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpGet("PayOutSalary")]
        public async Task<IActionResult> PayOutSalary(int id)
        {
            Worker chosenWorker = _workerService.GetWorkerById(id).Result;

            int orderCount = 0;

            foreach (RentOrder a in _workerService.GetWorkerCompletedOrders(id).Result)
            {
                orderCount++;
            }

            decimal workerSalary = chosenWorker.WorkerSalary(_workerService.GetWorkerBaseSalary(id).Result, orderCount);
            await _workerService.PayOutSalary(id, workerSalary);

            return Ok(workerSalary);

        }
    }
}
