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

        public List<Electric> ReadAllElectric()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            List<Electric> result = dbConnection.Query<Electric>(@"SELECT * FROM [dbo].[Elektromobiliai]").ToList();
            dbConnection.Close();
            return result;
        }

        public void WriteOneElectric(Electric electric)
        {
            string sqlCommand = "INSERT INTO Elektromobiliai ([Maker],[Model],[RentPrice],[BatteryCapacity],[ChargeTime]) VALUES " +
            "(@Maker, @Model, @RentPrice, @BatteryCapacity, @ChargeTime)";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, electric);
            }
        }

        public void WriteOneCombustion(Combustion combustion)
        {
            string sqlCommand = "INSERT INTO NaftosKuroAutomobiliai ([Maker],[Model],[RentPrice],[FuelConsumption]) VALUES " +
            "(@Maker, @Model, @RentPrice, @FuelConsumption)";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, combustion);
            }
        }


        public List<Combustion> ReadAllCombustion()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            List<Combustion> result = dbConnection.Query<Combustion>(@"SELECT * FROM [dbo].[NaftosKuroAutomobiliai]").ToList();
            dbConnection.Close();
            return result;

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
