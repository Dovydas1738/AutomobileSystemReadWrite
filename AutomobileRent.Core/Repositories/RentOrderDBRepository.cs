﻿using AutomobileRent.Core.Models;
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

        public List<RentOrder> ReadAllRentOrders()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            List<RentOrder> result = dbConnection.Query<RentOrder>(@"SELECT [Customer] AS CustomerId,[Car_id] AS CarId,[Type],[RentStart],[RentDuration],[RentPrice] FROM [dbo].[NuomosUzsakymai]").ToList();
            dbConnection.Close();
            return result;
        }

        public void WriteOneRentOrder(RentOrder rentOrder)
        {
            string sqlCommand = "INSERT INTO NuomosUzsakymai ([Customer],[Car_id],[Type],[RentStart],[RentDuration],[RentPrice]) VALUES " +
            "(@Customer, @Car_id, @Type, @RentStart, @RentDuration, @RentPrice)";

            var parameters = new
            {
                Customer = rentOrder.Customer.CustomerId,
                Car_id = rentOrder.Car.Id,
                Type = rentOrder.Type,
                RentStart = rentOrder.RentStart,
                RentDuration = rentOrder.RentDuration,
                RentPrice = rentOrder.RentPrice,
            };


            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }
        }

        public RentOrder GetRentOrderById(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            RentOrder result = dbConnection.QueryFirst<RentOrder>(@"SELECT * FROM [dbo].[NuomosUzsakymai] WHERE Id = @Id", new { Id = id });
            dbConnection.Close();
            return result;

        }

        public void RenewRentOrder(RentOrder rentOrder)
        {
            string sqlCommand = @"UPDATE [NuomosUzsakymai]
            SET [Customer] = @Customer
            ,[Car_id] = @Car_id
            ,[Type] = @Type
            ,[RentStart] = @RentStart
            ,[RentDuration] = @RentDuration
            ,[RentPrice] = @RentPrice
             WHERE Id = @Id";

            var parameters = new
            {
                Customer = rentOrder.Customer.CustomerId,
                Car_id = rentOrder.Car.Id,
                Type = rentOrder.Type,
                RentStart = rentOrder.RentStart,
                RentDuration = rentOrder.RentDuration,
                RentPrice = rentOrder.RentPrice,
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, parameters);
            }


        }


    }
}