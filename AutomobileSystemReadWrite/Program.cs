using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Repositories;
using AutomobileRent.Core.Services;
using AutomobileRent.Core.Models;
using System;
using AutomobileRent.Core.Enums;

namespace MyProgram
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            IWorkerDBRepository _workerRepository = new WorkerDBRepository("Server=localhost;Database=autonuoma;Trusted_Connection=True;");
            IWorkerService _workerService = new WorkerService(_workerRepository);
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
            Console.WriteLine("9. Renew customer data");
            Console.WriteLine("10. Renew rent order data");
            Console.WriteLine("11. See all rent orders");
            Console.WriteLine("12. Delete a customer");
            Console.WriteLine("13. Delete a car");
            Console.WriteLine("14. Delete an order");
            Console.WriteLine("15. Add a worker");
            Console.WriteLine("16. Edit worker's data");
            Console.WriteLine("17. Delete a worker");
            Console.WriteLine("18. See all workers");
            Console.WriteLine("19. Pay out a salary");
            Console.WriteLine("20. Exit");


            string choice2 = Console.ReadLine();

            while (choice2 != "20")
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
                            Console.WriteLine("Enter your worker Id");
                            int workerId = int.Parse(Console.ReadLine());

                            Worker servingWorker = _workerService.GetWorkerById(workerId);

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

                                RentOrder newOrder = new RentOrder(orderingCustomer, chosenCar, carType, DateTime.Now, duration, servingWorker);

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

                                RentOrder newOrder = new RentOrder(orderingCustomer, chosenCar, carType, DateTime.Now, duration, servingWorker);

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
                        Console.WriteLine(Math.Round(_rentService.GetTotalRentPrice(),2));

                        GetMenu();

                        choice2 = Console.ReadLine();



                        break;

                    case "8":
                        try
                        {
                            Console.WriteLine("Electric or combustion? (type e/c)");
                            string thisCaseChoice = Console.ReadLine();

                            if (thisCaseChoice == "e")
                            {
                                Console.WriteLine("Which car do you want to edit? (enter Id)");
                                int toChooseCarId = int.Parse(Console.ReadLine());

                                Car chosenCar = _autoRentService.GetElectricCarById(toChooseCarId);

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

                            else if(thisCaseChoice == "c")
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
                            else
                            {
                                Console.WriteLine("Wrong input");
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Wrong input");
                        }



                        GetMenu();
                        choice2 = Console.ReadLine();

                        break;

                    case "9":
                        try
                        {
                            Console.WriteLine("Which customer do you want to edit? (enter Id)");
                            int toChooseCustomerId = int.Parse(Console.ReadLine());

                            Customer chosenCustomer = _autoRentService.GetCustomerById(toChooseCustomerId);

                            Console.WriteLine("Enter customer's name (enter to skip)");
                            string nameChoice = Console.ReadLine();

                            if (nameChoice != "")
                            {
                                chosenCustomer.Name = nameChoice;
                            }

                            Console.WriteLine("Enter customer's surname (enter to skip)");
                            string surnameChoice = Console.ReadLine();

                            if (surnameChoice != "")
                            {
                                chosenCustomer.Surname = surnameChoice;
                            }


                            Console.WriteLine("Enter customer's date of birth (enter to skip)");

                            if (DateTime.TryParse(Console.ReadLine(), out DateTime birthDateChoice))
                            {
                                chosenCustomer.BirthDate = birthDateChoice;
                            }


                            _autoRentService.RenewCustomer(chosenCustomer);

                            Console.WriteLine("Customer info renewed successfully!");

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Wrong input");
                        }

                        GetMenu();
                        choice2 = Console.ReadLine();


                        break;

                    case "10":

                        try
                        {
                            Console.WriteLine("Which rent order do you want to edit? (enter Id)");
                            int toChooseRentOrderId = int.Parse(Console.ReadLine());

                            RentOrder chosenRentOrder = _autoRentService.GetOrderById(toChooseRentOrderId);

                            Console.WriteLine("Enter ordering customer's Id (skippas neveikia :( ))");

                            if (int.TryParse(Console.ReadLine(), out int customerIdChoice))
                            {
                                chosenRentOrder.Customer = _autoRentService.GetCustomerById(customerIdChoice);
                            }

                            Console.WriteLine("Enter customer's ordered car type e/c (enter to skip)");
                            string carChoice = Console.ReadLine();

                            if (carChoice == "e")
                            {
                                Console.WriteLine("Enter electric car Id (skippas neveikia :( )");

                                if (int.TryParse(Console.ReadLine(), out int customerCarIdChoice))
                                {
                                    chosenRentOrder.Car = _autoRentService.GetElectricCarById(customerCarIdChoice);
                                    chosenRentOrder.Type = "Electric";
                                }


                            }
                            else if(carChoice == "c")
                            {
                                Console.WriteLine("Enter combustion car Id (skippas neveikia :( )");

                                if (int.TryParse(Console.ReadLine(), out int customerCarIdChoice))
                                {
                                    chosenRentOrder.Car = _autoRentService.GetCombustionCarById(customerCarIdChoice);
                                    chosenRentOrder.Type = "Combustion";
                                }

                            }

                            Console.WriteLine("Enter new rental start time (enter to skip)");
                            if(DateTime.TryParse(Console.ReadLine(), out DateTime newRentStart))
                            {
                                chosenRentOrder.RentStart = newRentStart;
                            }

                            Console.WriteLine("Enter new rental duration (enter to skip)");
                            if (int.TryParse(Console.ReadLine(), out int newRentDuration))
                            {
                                chosenRentOrder.RentDuration = newRentDuration;
                            }

                            Console.WriteLine("Enter new worker Id (enter to skip)");
                            if (int.TryParse(Console.ReadLine(), out int newWorkerId))
                            {
                                chosenRentOrder.WorkerId = newWorkerId;
                            }


                            _autoRentService.RenewRentOrder(chosenRentOrder);
                            Console.WriteLine("Rent order data successfully updated!");

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Something went wrong");
                        }

                        GetMenu();
                        choice2 = Console.ReadLine();

                        break;

                    case "11":

                        foreach (RentOrder a in _autoRentService.GetAllRentOrders())
                        {
                            Console.WriteLine(a);
                        }

                        GetMenu();
                        choice2 = Console.ReadLine();

                        break;

                    case "12":

                        Console.WriteLine("Enter customer's Id that you want to delete");
                        int deletionId = int.Parse(Console.ReadLine());

                        _autoRentService.DeleteCustomerById(deletionId);

                        Console.WriteLine("Customer deleted successfully!");

                        GetMenu();
                        choice2 = Console.ReadLine();

                        break;

                    case "13":

                        Console.WriteLine("Delete electric or combustion? (e/c)");
                        string choiceCarDelete = Console.ReadLine();

                        if(choiceCarDelete == "e")
                        {
                            Console.WriteLine("Enter car id you want to delete");
                            int deleteId = int.Parse(Console.ReadLine());

                            _autoRentService.DeleteElectricCarById(deleteId);

                            Console.WriteLine("Car deleted successfully!");
                        }
                        else if (choiceCarDelete == "c")
                        {
                            Console.WriteLine("Enter car id you want to delete");
                            int deleteId = int.Parse(Console.ReadLine());

                            _autoRentService.DeleteCombustionCarById(deleteId);

                            Console.WriteLine("Car deleted successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Wrong input");
                        }

                        GetMenu();
                        choice2 = Console.ReadLine();

                        break;

                    case "14":


                        Console.WriteLine("Enter rent order Id that you want to delete");
                        int orderDeletionId = int.Parse(Console.ReadLine());

                        _autoRentService.DeleteRentOrderById(orderDeletionId);

                        Console.WriteLine("Order deleted successfully!");

                        GetMenu();
                        choice2 = Console.ReadLine();


                        break;

                    case "15":

                        Console.WriteLine("Enter new worker's name");
                        string newWorkerName = Console.ReadLine();

                        Console.WriteLine("Enter new worker's surname");
                        string newWorkerSurname = Console.ReadLine();

                        Console.WriteLine("Choose new worker's position:");
                        Console.WriteLine("1-Director; 2-Manager; 3-Mechanic");

                        WorkerPosition workerPosition = (WorkerPosition)int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter new workers base salary");
                        decimal newWorkerSalary = decimal.Parse(Console.ReadLine());

                        Worker newWorker = new Worker(newWorkerName, newWorkerSurname, workerPosition,newWorkerSalary);
                        _workerService.AddWorker(newWorker);

                        Worker createdWorker = _workerService.GetWorkerByNameSurname(newWorkerName, newWorkerSurname);

                     
                        _workerService.AddWorkersBaseSalary(createdWorker, newWorkerSalary);


                        Console.WriteLine("New worker added successfully!");

                        GetMenu();
                        choice2 = Console.ReadLine();

                        break;

                    case "16":

                        Console.WriteLine("Which worker do you want to edit? (enter Id)");
                        int toChooseWorkerId = int.Parse(Console.ReadLine());

                        Worker chosenWorkerToEdit = _workerService.GetWorkerById(toChooseWorkerId);

                        Console.WriteLine("Enter worker's name (enter to skip)");
                        string workerNameChoice = Console.ReadLine();

                        if (workerNameChoice != "")
                        {
                            chosenWorkerToEdit.Name = workerNameChoice;
                        }

                        Console.WriteLine("Enter worker's surname (enter to skip)");
                        string workerSurnameChoice = Console.ReadLine();

                        if (workerSurnameChoice != "")
                        {
                            chosenWorkerToEdit.Surname = workerSurnameChoice;
                        }

                        Console.WriteLine("Choose worker's position (enter to skip)");
                        Console.WriteLine("1-Director; 2-Manager; 3-Mechanic");
                        WorkerPosition positionChoice = (WorkerPosition)int.Parse(Console.ReadLine());

                        chosenWorkerToEdit.Position = positionChoice;

                        _workerService.RenewWorkerData(chosenWorkerToEdit);
                        Console.WriteLine("Worker edited successfully!");

                        GetMenu();
                        choice2 = Console.ReadLine();

                        break;

                    case "17":

                        Console.WriteLine("Enter worker Id you want to delete");
                        int workerIdToDelete = int.Parse(Console.ReadLine());

                        _workerService.DeleteWorkerById(workerIdToDelete);
                        Console.WriteLine("Worker deleted successfully!");

                        GetMenu();
                        choice2 = Console.ReadLine();

                        break;

                    case "18":

                        foreach (Worker a in _workerService.ReadWorkersDB())
                        {
                            Console.WriteLine(a);
                        }

                        GetMenu();
                        choice2 = Console.ReadLine();

                        break;

                    case "19":
                        Console.WriteLine("Enter worker Id");

                        int workerIdForOrders = int.Parse(Console.ReadLine());

                        Worker chosenWorker = _workerService.GetWorkerById(workerIdForOrders);

                        int orderCount = 0;

                        foreach(RentOrder a in _workerService.GetWorkerCompletedOrders(workerIdForOrders))
                        {
                            orderCount++;
                        }

                        decimal workerSalary = chosenWorker.WorkerSalary(_workerService.GetWorkerBaseSalary(workerIdForOrders), orderCount);
                        _workerService.PayOutSalary(workerIdForOrders, workerSalary);

                        Console.WriteLine($"Worker salary - {Math.Round(workerSalary,2)} Eur successfully paid out.");

                        GetMenu();
                        choice2 = Console.ReadLine();

                        break;

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
            Console.WriteLine("9. Renew customer data");
            Console.WriteLine("10. Renew rent order data");
            Console.WriteLine("11. See all rent orders");
            Console.WriteLine("12. Delete a customer");
            Console.WriteLine("13. Delete a car");
            Console.WriteLine("14. Delete an order");
            Console.WriteLine("15. Add a worker");
            Console.WriteLine("16. Edit worker's data");
            Console.WriteLine("17. Delete a worker");
            Console.WriteLine("18. See all workers");
            Console.WriteLine("19. Pay out a salary");
            Console.WriteLine("20. Exit");
        }


    }
}