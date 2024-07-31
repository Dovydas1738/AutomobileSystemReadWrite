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

            Console.WriteLine("What would you like to do?");
            Console.WriteLine();
            Console.WriteLine("1. See all cars");
            Console.WriteLine("2. See all customers");
            Console.WriteLine("3. Add a car");
            Console.WriteLine("4. Add a customer");
            Console.WriteLine("5. Make an order");
            Console.WriteLine("6. See customer's orders");
            Console.WriteLine("7. Get total price of all orders");
            Console.WriteLine("8. Exit");

            string choice2 = Console.ReadLine();

            while (choice2 != "8")
            {

                switch (choice2)
                {
                    case "1":

                        Console.WriteLine("All available cars: ");
                        List<Car> allCars = _autoRentService.GetCars();

                        foreach (Car car in allCars)
                        {
                            Console.WriteLine(car);
                        }


                        Console.WriteLine();
                        Console.WriteLine("Anything else?");
                        Console.WriteLine();
                        Console.WriteLine("1. See all cars");
                        Console.WriteLine("2. See all customers");
                        Console.WriteLine("3. Add a car");
                        Console.WriteLine("4. Add a customer");
                        Console.WriteLine("5. Make an order");
                        Console.WriteLine("6. See customer's orders");
                        Console.WriteLine("7. Get total price of all orders");
                        Console.WriteLine("8. Exit");

                        choice2 = Console.ReadLine();

                        break;

                    case "2":

                        Console.WriteLine("All customers: ");
                        List<Customer> allCustomers = _customersService.GetAllCustomers();

                        foreach (Customer customer in allCustomers)
                        {
                            Console.WriteLine(customer);
                        }


                        Console.WriteLine();
                        Console.WriteLine("Anything else?");
                        Console.WriteLine();
                        Console.WriteLine("1. See all cars");
                        Console.WriteLine("2. See all customers");
                        Console.WriteLine("3. Add a car");
                        Console.WriteLine("4. Add a customer");
                        Console.WriteLine("5. Make an order");
                        Console.WriteLine("6. See customer's orders");
                        Console.WriteLine("7. Get total price of all orders");
                        Console.WriteLine("8. Exit");

                        choice2 = Console.ReadLine();



                        break;

                    case "3":

                        Console.WriteLine("Enter car Id");
                        int newId = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter car Maker");
                        string newMaker = Console.ReadLine();

                        Console.WriteLine("Enter car Model");
                        string newModel = Console.ReadLine();

                        Console.WriteLine("Enter Rent Price");
                        decimal newRentPrice = decimal.Parse(Console.ReadLine());

                        Console.WriteLine("Is the car Electric or Combustion engine? (type e/c)");
                        string choice = Console.ReadLine();

                        if (choice == "e")
                        {
                            Console.WriteLine("Enter Battery capacity");
                            int newBatteryCapacity = int.Parse(Console.ReadLine());

                            Console.WriteLine("Enter Charge Time");
                            decimal newChargeTime = decimal.Parse(Console.ReadLine());

                            Car newCar = new Electric(newId, newMaker, newModel, newRentPrice, newBatteryCapacity, newChargeTime);
                            _autoRentService.AddNewCar(newCar);

                            Console.WriteLine("Car created successfully!");

                        }
                        else if (choice == "c")
                        {
                            Console.WriteLine("Enter Fuel Consumption");
                            decimal newFuelConsumption = decimal.Parse(Console.ReadLine());

                            Car newCar = new Combustion(newId, newMaker, newModel, newRentPrice, newFuelConsumption);
                            _autoRentService.AddNewCar(newCar);

                            Console.WriteLine("Car created successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Wrong input");
                        }

                        Console.WriteLine();
                        Console.WriteLine("Anything else?");
                        Console.WriteLine();
                        Console.WriteLine("1. See all cars");
                        Console.WriteLine("2. See all customers");
                        Console.WriteLine("3. Add a car");
                        Console.WriteLine("4. Add a customer");
                        Console.WriteLine("5. Make an order");
                        Console.WriteLine("6. See customer's orders");
                        Console.WriteLine("7. Get total price of all orders");
                        Console.WriteLine("8. Exit");

                        choice2 = Console.ReadLine();


                        break;

                    case "4":
                        Console.WriteLine("Customer's first name");
                        string name = Console.ReadLine();

                        Console.WriteLine("Customer's surname");
                        string surname = Console.ReadLine();

                        Console.WriteLine("Customer's date of birth");
                        DateOnly birthdate = DateOnly.Parse(Console.ReadLine());

                        bool doesExist = false;

                        foreach (Customer oldCustomer in _customersService.GetAllCustomers())
                        {
                            if (oldCustomer.Name == name && oldCustomer.Surname == surname)
                            {
                                Console.WriteLine("Customer already exists");
                                doesExist = true;
                                break;
                            }

                        }

                        if (doesExist == false)
                        {
                            Customer newCustomer = new Customer(name, surname, birthdate);
                            _customersService.AddCustomer(newCustomer);

                            Console.WriteLine("Customer created successfully!");

                        }

                        Console.WriteLine();
                        Console.WriteLine("Anything else?");
                        Console.WriteLine();
                        Console.WriteLine("1. See all cars");
                        Console.WriteLine("2. See all customers");
                        Console.WriteLine("3. Add a car");
                        Console.WriteLine("4. Add a customer");
                        Console.WriteLine("5. Make an order");
                        Console.WriteLine("6. See customer's orders");
                        Console.WriteLine("7. Get total price of all orders");
                        Console.WriteLine("8. Exit");

                        choice2 = Console.ReadLine();


                        break;

                    case "5":

                        try
                        {
                            Console.WriteLine("Customer's name");
                            name = Console.ReadLine();
                            Console.WriteLine("Customer's surname");
                            surname = Console.ReadLine();

                            _customersService.SearchByNameSurname(name, surname);

                            Customer orderingCustomer = _customersService.SearchByNameSurname(name, surname)[0];


                            Console.WriteLine("Which car would the customer like to rent? (enter maker)");
                            string toChooseCar = Console.ReadLine();



                            if (_carsService.SearchByMaker(toChooseCar)[0] != null)
                            {
                                Car chosenCar = _carsService.SearchByMaker(toChooseCar)[0];

                                Console.WriteLine("Enter rent duration (days)");
                                int duration = int.Parse(Console.ReadLine());

                                RentOrder newOrder = new RentOrder(orderingCustomer, chosenCar, DateTime.Now, duration);
                                _rentService.CreateOrder(newOrder);

                                Console.WriteLine("Order was successful!");
                                Console.WriteLine($"Price: {newOrder.CountRentPrice()}");
                                Console.WriteLine($"Rental ends: {newOrder.GetRentEndDate()}");

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Something went wrong");
                        }

                        Console.WriteLine();
                        Console.WriteLine("Anything else?");
                        Console.WriteLine();
                        Console.WriteLine("1. See all cars");
                        Console.WriteLine("2. See all customers");
                        Console.WriteLine("3. Add a car");
                        Console.WriteLine("4. Add a customer");
                        Console.WriteLine("5. Make an order");
                        Console.WriteLine("6. See customer's orders");
                        Console.WriteLine("7. Get total price of all orders");
                        Console.WriteLine("8. Exit");

                        choice2 = Console.ReadLine();

                        break;

                    case "6":
                        Console.WriteLine("Enter customer's name");
                        string nameSearch = Console.ReadLine();

                        bool hasOrders = false;

                        foreach (RentOrder a in _rentService.GetAllOrders())
                        {
                            if (a.Customer.Name == nameSearch)
                            {
                                foreach(RentOrder order in _rentService.GetOrdersByCustomer(a.Customer))
                                {
                                    Console.WriteLine(order);
                                }

                                
                                hasOrders = true;
                            }

                        }

                        if (hasOrders == false)
                        {
                            Console.WriteLine("Customer has no orders");
                        }

                        Console.WriteLine();
                        Console.WriteLine("Anything else?");
                        Console.WriteLine();
                        Console.WriteLine("1. See all cars");
                        Console.WriteLine("2. See all customers");
                        Console.WriteLine("3. Add a car");
                        Console.WriteLine("4. Add a customer");
                        Console.WriteLine("5. Make an order");
                        Console.WriteLine("6. See customer's orders");
                        Console.WriteLine("7. Get total price of all orders");
                        Console.WriteLine("8. Exit");

                        choice2 = Console.ReadLine();



                        break;

                    case "7":

                        Console.WriteLine("Total price of all rentals:");
                        Console.WriteLine(_rentService.GetTotalRentPrice());

                        Console.WriteLine();
                        Console.WriteLine("Anything else?");
                        Console.WriteLine();
                        Console.WriteLine("1. See all cars");
                        Console.WriteLine("2. See all customers");
                        Console.WriteLine("3. Add a car");
                        Console.WriteLine("4. Add a customer");
                        Console.WriteLine("5. Make an order");
                        Console.WriteLine("6. See customer's orders");
                        Console.WriteLine("7. Get total price of all orders");
                        Console.WriteLine("8. Exit");

                        choice2 = Console.ReadLine();



                        break;
                }

            }

        }
    }
}