using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

namespace AutomobileRent.Core.Repositories
{
    public class CarsDBRepository : ICarsRepository
    {
        private readonly string _dbConnectionString;
        public CarsDBRepository(string connectionString)
        {
            _dbConnectionString = connectionString;
        }


        public List<Car> ReadCars()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Electric>> ReadAllElectric()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = await dbConnection.
            QueryAsync<Electric>(@"SELECT * FROM [dbo].[Elektromobiliai]");
            dbConnection.Close();
            return result.ToList();
        }

        public async Task WriteOneElectric(Electric electric)
        {
            string sqlCommand = "INSERT INTO Elektromobiliai ([Maker],[Model],[RentPrice],[BatteryCapacity],[ChargeTime]) VALUES " +
            "(@Maker, @Model, @RentPrice, @BatteryCapacity, @ChargeTime)";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                await connection.ExecuteAsync(sqlCommand, electric);
            }
        }

        public async Task WriteOneCombustion(Combustion combustion)
        {
            string sqlCommand = "INSERT INTO NaftosKuroAutomobiliai ([Maker],[Model],[RentPrice],[FuelConsumption]) VALUES " +
            "(@Maker, @Model, @RentPrice, @FuelConsumption)";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                await connection.ExecuteAsync(sqlCommand, combustion);
            }
        }


        public async Task<List<Combustion>> ReadAllCombustion()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = await dbConnection.QueryAsync<Combustion>(@"SELECT * FROM [dbo].[NaftosKuroAutomobiliai]");
            dbConnection.Close();
            return result.ToList();

        }



        public async Task<Electric> GetElectricCarById(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = await dbConnection.QueryFirstAsync<Electric>(@"SELECT * FROM [dbo].[Elektromobiliai] WHERE Id = @Id", new {Id=id});
            dbConnection.Close();
            return result;

        }

        public async Task<Combustion> GetCombustionCarById(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            var result = await dbConnection.QueryFirstAsync<Combustion>(@"SELECT * FROM [dbo].[NaftosKuroAutomobiliai] WHERE Id = @Id", new {Id=id});
            dbConnection.Close();
            return result;

        }

        public async Task RenewElectric(Electric electric)
        {
            string sqlCommand = @"UPDATE [Elektromobiliai]
            SET [Maker] = @Maker
            ,[Model] = @Model
            ,[RentPrice] = @RentPrice
            ,[BatteryCapacity] = @BatteryCapacity
            ,[ChargeTime] = @ChargeTime
            WHERE Id = @Id";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                await connection.ExecuteAsync(sqlCommand, electric);
            }
        }

        public async Task RenewCombustion(Combustion combustion)
        {
            string sqlCommand = @"UPDATE [NaftosKuroAutomobiliai]
            SET [Maker] = @Maker
            ,[Model] = @Model
            ,[RentPrice] = @RentPrice
            ,[FuelConsumption] = @FuelConsumption
            WHERE Id = @Id";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                await connection.ExecuteAsync(sqlCommand, combustion);
            }
        }

        public async Task DeleteElectricById(int id)
        {
            string sqlCommand = "DELETE FROM Elektromobiliai WHERE Id = @id";

            var parameters = new
            {
                id = id
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                await connection.ExecuteAsync(sqlCommand, parameters);
            }
        }

        public async Task DeleteCombustionById(int id)
        {
            string sqlCommand = "DELETE FROM NaftosKuroAutomobiliai WHERE Id = @id";

            var parameters = new
            {
                id = id
            };

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                await connection.ExecuteAsync(sqlCommand, parameters);
            }
        }


        public void WriteCars(List<Car> cars)
        {
            throw new NotImplementedException();
        }

        public void WriteOneCar(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
