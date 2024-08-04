using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileRent.Core.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerDBRepository _workerRepository;

        public WorkerService(IWorkerDBRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }



        public Worker GetWorkerById(int id)
        {
            return _workerRepository.GetWorkerById(id);
        }

        public List<Worker> ReadWorkersDB()
        {
            return _workerRepository.ReadWorkersDB();
        }
    }
}
