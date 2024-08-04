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


    }
}
