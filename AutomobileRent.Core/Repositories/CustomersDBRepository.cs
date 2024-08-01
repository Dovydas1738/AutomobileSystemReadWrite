using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Models;
using System.Data;

namespace AutomobileRent.Core.Repositories
{
    public class CustomersDBRepository : ICustomersRepository
    {
        private readonly string _dbConnectionString;
        public CustomersDBRepository(string connectionString)
        {
            _dbConnectionString = connectionString;
        }

        public List<Customer> ReadCustomersDB()
        {
            using IDbConnection dbConnection = new SqlConnection(_dbConnectionString);
            dbConnection.Open();
            List<Customer> result = dbConnection.Query<Customer>(@"SELECT [Id] AS CustomerId, [Name], [Surname], [BirthDate] FROM [dbo].[Klientai]").ToList();
            dbConnection.Close();
            return result;
        }

        public void WriteCustomerDB(Customer customer)
        {
            string sqlCommand = "INSERT INTO Klientai ([Name], [Surname], [BirthDate]) VALUES " +
            "(@Name, @Surname, @BirthDate)";

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Execute(sqlCommand, customer);
            }
        }

        public List<Customer> ReadCustomers()
        {
            throw new NotImplementedException();
        }

        public void WriteCustomers(List<Customer> customers)
        {
            throw new NotImplementedException();
        }

        public void WriteOneCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
