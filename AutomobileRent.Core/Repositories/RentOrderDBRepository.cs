using AutomobileRent.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using AutomobileRent.Core.Contracts;

namespace AutomobileRent.Core.Repositories
{
    public class RentOrderDBRepository : IRentOrderRepository
    {
        private readonly string _dbConnectionString;
        public RentOrderDBRepository(string connectionString)
        {
            _dbConnectionString = connectionString;
        }

        public async Task<List<RentOrder>> ReadAllRentOrders()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = await dbConnection.QueryAsync<RentOrder>(@"SELECT [Id], [Customer] AS CustomerId,[Car_id] AS CarId,[Type],[RentStart],[RentDuration],[RentPrice],[WorkerId] FROM [dbo].[NuomosUzsakymai]");
            dbConnection.Close();
            return result.ToList();
        }

        public async Task WriteOneRentOrder(RentOrder rentOrder)
        {
            string sqlCommand = "INSERT INTO NuomosUzsakymai ([Customer],[Car_id],[Type],[RentStart],[RentDuration],[RentPrice],[WorkerId]) VALUES " +
            "(@Customer, @Car_id, @Type, @RentStart, @RentDuration, @RentPrice, @WorkerId)";

            var parameters = new
            {
                Customer = rentOrder.Customer.CustomerId,
                Car_id = rentOrder.Car.Id,
                Type = rentOrder.Type,
                RentStart = rentOrder.RentStart,
                RentDuration = rentOrder.RentDuration,
                RentPrice = rentOrder.RentPrice,
                WorkerId = rentOrder.Worker.Id,
            };


            using (var connection = new SqlConnection(_dbConnectionString))
            {
                await connection.ExecuteAsync(sqlCommand, parameters);
            }
        }

        public async Task<RentOrder> GetRentOrderById(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = await dbConnection.QueryFirstAsync<RentOrder>(@"SELECT [Id], [Customer] AS CustomerId,[Car_id] AS CarId,[Type],[RentStart],[RentDuration],[RentPrice],[WorkerId] FROM [dbo].[NuomosUzsakymai] WHERE [Id] = @Id", new { Id = id });
            dbConnection.Close();
            return result;

        }

        public async Task RenewRentOrder(RentOrder rentOrder)
        {
            string sqlCommand = @"UPDATE [NuomosUzsakymai]
            SET [Customer] = @Customer
            ,[Car_id] = @Car_id
            ,[Type] = @Type
            ,[RentStart] = @RentStart
            ,[RentDuration] = @RentDuration
            ,[RentPrice] = @RentPrice
            ,[WorkerId] = @WorkerId
             WHERE Id = @Id";

            var parameters = new
            {
                Customer = rentOrder.Customer.CustomerId,
                Car_id = rentOrder.Car.Id,
                Type = rentOrder.Type,
                RentStart = rentOrder.RentStart,
                RentDuration = rentOrder.RentDuration,
                RentPrice = rentOrder.RentPrice,
                Id = rentOrder.Id,
                WorkerId = rentOrder.WorkerId,
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                await connection.ExecuteAsync(sqlCommand, parameters);
            }


        }

        public async Task DeleteRentOrderById(int id)
        {
            string sqlCommand = "DELETE FROM NuomosUzsakymai WHERE Id = @id";

            var parameters = new
            {
                id = id
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                await connection.ExecuteAsync(sqlCommand, parameters);
            }
        }



    }
}
