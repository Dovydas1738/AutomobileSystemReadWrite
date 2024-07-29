using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Repositories;
using AutomobileRent.Core.Services;
using AutomobileRent.Core.Models;
using System;

namespace MyProgram
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            
            IRentService _rentService = new RentService();
            ICarsRepository _carsRepository = new CarsRepository("C:\\Users\\dovis\\source\\repos\\AutomobileSystemReadWrite\\AutomobileRent.Core\\Repositories\\CarsData.txt");
            ICustomersRepository _customersRepository = new CustomersRepository("C:\\Users\\dovis\\source\\repos\\AutomobileSystemReadWrite\\AutomobileRent.Core\\Repositories\\CustomersData.txt");
            ICarsService _carsService = new CarsService(_carsRepository);
            ICustomersService _customersService = new CustomersService(_customersRepository);
            AutoRentService _autoRentService = new AutoRentService(_carsService, _customersService, _rentService);



            Console.WriteLine("Welcome to Late Night Car Rentals!");
            Console.WriteLine("Are you a customer/worker? (type c/w)");
            string choice1 = Console.ReadLine();

            if(choice1 == "c")
            {

                Console.WriteLine("What would you like to do?");
                Console.WriteLine();
                Console.WriteLine("1. Check out our cars and prices");
                Console.WriteLine("2. Place an order");
                Console.WriteLine("3. See your orders");
                Console.WriteLine("4. Exit");

                string choice2 = Console.ReadLine();

                while (choice2 != "4")
                {

                    switch (choice2)
                    {
                        case "1":
                            Console.WriteLine("All available cars: ");
                            _carsRepository.ReadCars();
                            Console.WriteLine(_autoRentService.GetCars());

                            Console.WriteLine();
                            Console.WriteLine("Anything else?");
                            Console.WriteLine();
                            Console.WriteLine("1. Check out our cars and prices");
                            Console.WriteLine("2. Place an order");
                            Console.WriteLine("3. See your orders");
                            Console.WriteLine("4. Exit");

                            choice2 = Console.ReadLine();
                            break;

                        case "2":
                            Console.WriteLine("What is your first name?");
                            string name = Console.ReadLine();

                            Console.WriteLine("What is your surname?");
                            string surname = Console.ReadLine();

                            Console.WriteLine("What is date of birth?");
                            DateOnly birthdate = DateOnly.Parse(Console.ReadLine());

                            Customer newCustomer = new Customer(name,surname,birthdate);
                            _customersService.AddCustomer(newCustomer);
                            break;

                    }
                }

            }

            else if(choice1 == "w")
            {

            }

            else
            {
                Console.WriteLine("Wrong input");
            }







        }


        
    }
}