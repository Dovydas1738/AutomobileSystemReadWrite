using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace AutomobileRent.Core.Repositories
{
    public class WorkerDBRepository : IWorkerDBRepository
    {
        private readonly string _dbConnectionString;
        public WorkerDBRepository(string connectionString)
        {
            _dbConnectionString = connectionString;
        }

        public List<Worker> ReadWorkersDB()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            List<Worker> result = dbConnection.Query<Worker>(@"SELECT * FROM dbo.Darbuotojai").ToList();
            dbConnection.Close();
            return result;
        }

        public Worker GetWorkerById(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            Worker result = dbConnection.QueryFirst<Worker>(@"SELECT * FROM dbo.Darbuotojai WHERE Id = @Id", new { Id = id });
            dbConnection.Close();
            return result;

        }

        public Worker GetWorkerByNameSurname(string name, string surname)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            Worker result = dbConnection.QueryFirst<Worker>(@"SELECT * FROM dbo.Darbuotojai WHERE Name = @Name AND Surname = @Surname", new { Name = name, Surname = surname });
            dbConnection.Close();
            return result;

        }


        public decimal GetWorkerBaseSalary(int workerId)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            decimal result = dbConnection.QueryFirst<decimal>(@"SELECT BaseSalary FROM dbo.DarbuotojuAtlyginimai WHERE WorkerId = @WorkerId", new { WorkerId = workerId });
            dbConnection.Close();
            return result;

        }

        public void UpdateWorkerBaseSalary(Worker worker, decimal salary)
        {
            string sqlCommand = @"UPDATE [DarbuotojuAtlyginimai]
            SET [BaseSalary] = @BaseSalary
             WHERE WorkerId = @WorkerId";

            var parameters = new
            {
                WorkerId = worker.Id,
                BaseSalary = salary,
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }


        public void AddWorkersBaseSalary (Worker worker, decimal salary)
        {
            string sqlCommand = "INSERT INTO DarbuotojuAtlyginimai ([WorkerId], [BaseSalary]) VALUES " +
            "(@WorkerId, @BaseSalary)";

            var parameters = new
            {
                WorkerId = worker.Id,
                BaseSalary = salary,
            };


            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }

        }

        public List<RentOrder> GetWorkerCompletedOrders(int workerId)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            List<RentOrder> result = dbConnection.Query<RentOrder>(@"SELECT [Id], [Customer] AS CustomerId,[Car_id] AS CarId,[Type],[RentStart],[RentDuration],[RentPrice],[WorkerId] FROM dbo.NuomosUzsakymai WHERE WorkerId = @WorkerId", new { WorkerId = workerId }).ToList();
            dbConnection.Close();
            return result;

        }

        public void PayOutSalary(int workerId, decimal workerSalary)
        {
            string sqlCommand = "INSERT INTO IsmokamiAtlyginimai (WorkerId, Salary, PaymentDate) VALUES " +
            "(@WorkerId, @Salary, @PaymentDate)";

            var parameters = new
            {
                WorkerId = workerId,
                Salary = workerSalary,
                PaymentDate = DateTime.Now,
            };


            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }

        }

        public void AddWorker(Worker worker)
        {
            string sqlCommand = "INSERT INTO Darbuotojai ([Name], [Surname], [Position]) VALUES " +
            "(@Name, @Surname, @Position)";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, worker);
            }

        }

        public void RenewWorkerData(Worker worker)
        {
            string sqlCommand = @"UPDATE [Darbuotojai]
            SET [Name] = @Name
            ,[Surname] = @Surname
            ,[Position] = @Position
             WHERE Id = @Id";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, worker);
            }
        }

        public void DeleteWorkerById(int workerId)
        {
            string sqlCommand = "DELETE FROM Darbuotojai WHERE Id = @id";

            var parameters = new
            {
                id = workerId,
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }

        }
    }
}
