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
            IRentOrderRepository _rentOrderRepository = new RentOrderDBRepository("Server=localhost;Database=autonuoma;Trusted_Connection=True;");
            IRentService _rentService = new RentService(_rentOrderRepository);
            //ICarsRepository _carsRepository = new CarsRepository("C:\\Users\\dovis\\source\\repos\\AutomobileSystemReadWrite\\AutomobileRent.Core\\Repositories\\CarsData.txt");
            ICarsRepository _carsRepository = new CarsDBRepository("Server=localhost;Database=autonuoma;Trusted_Connection=True;");
            //ICustomersRepository _customersRepository = new CustomersRepository("C:\\Users\\dovis\\source\\repos\\AutomobileSystemReadWrite\\AutomobileRent.Core\\Repositories\\CustomersData.txt");
            ICustomersRepository _customersRepository = new CustomersDBRepository("Server=localhost;Database=autonuoma;Trusted_Connection=True;");
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
            Console.WriteLine("8. Renew car data");
            Console.WriteLine("9. Exit");


            string choice2 = Console.ReadLine();

            while (choice2 != "9")
            {

                switch (choice2)
                {
                    case "1":
                        Console.WriteLine("All available combustion cars: ");
                        List<Combustion> allCombustionCars = _autoRentService.GetAllCombustion();

                        foreach (Car car in allCombustionCars)
                        {
                            Console.WriteLine(car);
                        }

                        Console.WriteLine("All available electric cars: ");
                        List<Electric> allElectricCars = _autoRentService.GetAllElectric();

                        foreach (Car car in allElectricCars)
                        {
                            Console.WriteLine(car);
                        }


                        GetMenu();

                        choice2 = Console.ReadLine();

                        break;

                    case "2":

                        Console.WriteLine("All customers: ");
                        List<Customer> allCustomers = _autoRentService.GetAllCustomers();

                        foreach (Customer customer in allCustomers)
                        {
                            Console.WriteLine(customer);
                        }


                        GetMenu();

                        choice2 = Console.ReadLine();



                        break;

                    case "3":

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

                            Electric newCar = new Electric(newMaker, newModel, newRentPrice, newBatteryCapacity, newChargeTime);
                            _autoRentService.AddNewElectric(newCar);

                            Console.WriteLine("Car created successfully!");

                        }
                        else if (choice == "c")
                        {
                            Console.WriteLine("Enter Fuel Consumption");
                            decimal newFuelConsumption = decimal.Parse(Console.ReadLine());

                            Combustion newCar = new Combustion(newMaker, newModel, newRentPrice, newFuelConsumption);
                            _autoRentService.AddNewCombustion(newCar);

                            Console.WriteLine("Car created successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Wrong input");
                        }

                        GetMenu();

                        choice2 = Console.ReadLine();


                        break;

                    case "4":
                        Console.WriteLine("Customer's first name");
                        string name = Console.ReadLine();

                        Console.WriteLine("Customer's surname");
                        string surname = Console.ReadLine();

                        Console.WriteLine("Customer's date of birth");
                        DateTime birthdate = DateTime.Parse(Console.ReadLine());

                        bool doesExist = false;

                        foreach (Customer oldCustomer in _autoRentService.GetAllCustomers())
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
                            _autoRentService.AddNewCustomer(newCustomer);

                            Console.WriteLine("Customer created successfully!");

                        }

                        GetMenu();

                        choice2 = Console.ReadLine();


                        break;

                    case "5":

                        try
                        {
                            Console.WriteLine("Customer's name");
                            name = Console.ReadLine();
                            Console.WriteLine("Customer's surname");
                            surname = Console.ReadLine();


                            Customer orderingCustomer = _customersService.SearchByNameSurname(name, surname)[0];

                            Console.WriteLine("Electric or combustion? (type Electric/Combustion)");
                            string carType = Console.ReadLine();

                            if (carType == "Electric")
                            {
                                Console.WriteLine("Which car would the customer like to rent? (enter Id)");
                                int toChooseCarId = int.Parse(Console.ReadLine());

                                Car chosenCar = _autoRentService.GetElectricCarById(toChooseCarId);

                                Console.WriteLine("Enter rent duration (days)");
                                int duration = int.Parse(Console.ReadLine());

                                RentOrder newOrder = new RentOrder(orderingCustomer, chosenCar, carType, DateTime.Now, duration);

                                _autoRentService.AddOneRentOrder(newOrder);

                                Console.WriteLine("Order was successful!");
                                Console.WriteLine($"Price: {newOrder.CountRentPrice()}");
                                Console.WriteLine($"Rental ends: {newOrder.GetRentEndDate()}");

                            }

                            else if (carType == "Combustion")
                            {
                                Console.WriteLine("Which car would the customer like to rent? (enter Id)");
                                int toChooseCarId = int.Parse(Console.ReadLine());

                                Car chosenCar = _autoRentService.GetElectricCarById(toChooseCarId);

                                Console.WriteLine("Enter rent duration (days)");
                                int duration = int.Parse(Console.ReadLine());

                                RentOrder newOrder = new RentOrder(orderingCustomer, chosenCar, carType, DateTime.Now, duration);

                                _autoRentService.AddOneRentOrder(newOrder);

                                Console.WriteLine("Order was successful!");
                                Console.WriteLine($"Price: {newOrder.CountRentPrice()}");
                                Console.WriteLine($"Rental ends: {newOrder.GetRentEndDate()}");
                            }

                            else
                            {
                                Console.WriteLine("Something went wrong");
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Something went wrong");
                        }

                        GetMenu();

                        choice2 = Console.ReadLine();

                        break;

                    case "6":
                        Console.WriteLine("Enter customer's name");
                        string nameSearch = Console.ReadLine();
                        Console.WriteLine("Enter customer's surname");
                        string surnameSearch = Console.ReadLine();

                        int customerId = 0;

                        foreach (Customer b in _autoRentService.GetAllCustomers())
                        {
                            if (b.Name == nameSearch && b.Surname == surnameSearch)
                            {
                                customerId = b.CustomerId;
                            }
                        }

                        if(customerId != 0)
                        {
                            foreach (RentOrder a in _autoRentService.GetAllRentOrders())
                            {
                                if (customerId == a.CustomerId)
                                {
                                    Console.WriteLine(a);
                                }

                            }
                        }

                        else
                        {
                            Console.WriteLine("Customer has no orders");
                        }

                        GetMenu();

                        choice2 = Console.ReadLine();



                        break;

                    case "7":

                        Console.WriteLine("Total price of all rentals:");
                        Console.WriteLine(_rentService.GetTotalRentPrice());

                        GetMenu();

                        choice2 = Console.ReadLine();



                        break;

                    case "8":

                        Console.WriteLine("Electric or combustion? (type e/c)");
                        string thisCaseChoice = Console.ReadLine();

                        if (thisCaseChoice == "e")
                        {
                            Console.WriteLine("Which car do you want to edit? (enter Id)");
                            int toChooseCarId = int.Parse(Console.ReadLine());

                            Car chosenCar = _autoRentService.GetElectricCarById(toChooseCarId);

                            Console.WriteLine("Enter car Maker (enter to skip)");
                            string makerChoice = Console.ReadLine();

                            if(makerChoice != "")
                            {
                                chosenCar.Maker = makerChoice;
                            }

                            Console.WriteLine("Enter car Model (enter to skip)");
                            string modelChoice = Console.ReadLine();

                            if (modelChoice != "")
                            {
                                chosenCar.Model = modelChoice;
                            }

                            Console.WriteLine("Enter Rent Price (enter to skip)");

                            if (decimal.TryParse(Console.ReadLine(), out decimal rentPriceChoice))
                            {
                                chosenCar.RentPrice = rentPriceChoice;
                            }

                            Console.WriteLine("Enter Battery capacity (enter to skip)");

                            if (int.TryParse(Console.ReadLine(), out int batteryCapacityChoice))
                            {
                                ((Electric)chosenCar).BatteryCapacity = batteryCapacityChoice;
                            }


                            Console.WriteLine("Enter Charge time (enter to skip)");

                            if (int.TryParse(Console.ReadLine(), out int chargeTimeChoice))
                            {
                                ((Electric)chosenCar).ChargeTime = chargeTimeChoice;
                            }

                            _autoRentService.RenewElectric((Electric)chosenCar);

                            Console.WriteLine("Car info renewed successfully!");
                        }

                        else
                        {
                            Console.WriteLine("Which car do you want to edit? (enter Id)");
                            int toChooseCarId = int.Parse(Console.ReadLine());

                            Car chosenCar = _autoRentService.GetCombustionCarById(toChooseCarId);

                            Console.WriteLine("Enter car Maker (enter to skip)");
                            string makerChoice = Console.ReadLine();

                            if (makerChoice != "")
                            {
                                chosenCar.Maker = makerChoice;
                            }

                            Console.WriteLine("Enter car Model (enter to skip)");
                            string modelChoice = Console.ReadLine();

                            if (modelChoice != "")
                            {
                                chosenCar.Model = modelChoice;
                            }

                            Console.WriteLine("Enter Rent Price (enter to skip)");

                            if (decimal.TryParse(Console.ReadLine(), out decimal rentPriceChoice))
                            {
                                chosenCar.RentPrice = rentPriceChoice;
                            }

                            Console.WriteLine("Enter Fuel Consumption 0.0 format (enter to skip)");

                            if (decimal.TryParse(Console.ReadLine(), out decimal fuelConsumptionChoice))
                            {
                                ((Combustion)chosenCar).FuelConsumption = fuelConsumptionChoice;
                            }


                            _autoRentService.RenewCombustion((Combustion)chosenCar);

                            Console.WriteLine("Car info renewed successfully!");

                        }

                        GetMenu();
                        choice2 = Console.ReadLine();

                        break;

                    //case "9":

                    //    Console.WriteLine("All available electric cars: ");
                    //    List<Electric> allElectricCars = _autoRentService.GetAllElectric();

                    //    foreach (Car car in allElectricCars)
                    //    {
                    //        Console.WriteLine(car);
                    //    }

                    //    GetMenu();

                    //    choice2 = Console.ReadLine();


                    //    break;

                    //case "10":

                    //    Console.WriteLine("All available combustion cars: ");
                    //    List<Combustion> allCombustionCars = _autoRentService.GetAllCombustion();

                    //    foreach (Car car in allCombustionCars)
                    //    {
                    //        Console.WriteLine(car);
                    //    }

                    //    GetMenu();

                    //    choice2 = Console.ReadLine();


                    //    break;
                }

            }

        }

        public static void GetMenu()
        {
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
            Console.WriteLine("8. Renew car data");
            Console.WriteLine("9. Exit");

        }


    }
}