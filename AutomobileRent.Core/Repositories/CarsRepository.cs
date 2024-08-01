using AutomobileRent.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomobileRent.Core.Models;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.ConstrainedExecution;

namespace AutomobileRent.Core.Repositories
{
    public class CarsRepository : ICarsRepository
    {
        private readonly string _filePath;

        public CarsRepository(string carsFilePath)
        {
            _filePath = carsFilePath;
        }

        public Combustion GetCombustionCarById(int id)
        {
            throw new NotImplementedException();
        }

        public Electric GetElectricCarById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Combustion> ReadAllCombustion()
        {
            throw new NotImplementedException();
        }

        public List<Electric> ReadAllElectric()
        {
            throw new NotImplementedException();
        }

        public List<Car> ReadCars()
        {
            List<Car> cars = new List<Car>();

            using (StreamReader customerReader = new StreamReader(this._filePath))
            {
                while (!customerReader.EndOfStream)
                {
                    string line = customerReader.ReadLine();
                    string[] values = line.Split(",");

                    if(values.Length > 5)
                    {
                        cars.Add(new Electric(int.Parse(values[0]), values[1], values[2], int.Parse(values[3]), int.Parse(values[4]), decimal.Parse(values[5])));
                    }
                    else
                    {
                        cars.Add(new Combustion(int.Parse(values[0]), values[1], values[2], int.Parse(values[3]), decimal.Parse(values[4])));
                    }
                    
                }
                return cars;
            }

        }

        public void RenewCombustion(Combustion combustion)
        {
            throw new NotImplementedException();
        }

        public void RenewElectric(Electric electric)
        {
            throw new NotImplementedException();
        }

        public void WriteCars(List<Car> cars)
        {
            foreach (Car car in cars)
            {
                if (car is Electric)
                {
                    using (StreamWriter carWriter = new StreamWriter(this._filePath, true))
                    {
                        carWriter.WriteLine($"{car.Id},{car.Maker},{car.Model},{car.RentPrice},{((Electric)car).BatteryCapacity},{((Electric)car).ChargeTime}");
                    }

                }
                else
                {
                    using (StreamWriter carWriter = new StreamWriter(this._filePath, true))
                    {
                        carWriter.WriteLine($"{car.Id},{car.Maker},{car.Model},{car.RentPrice},{((Combustion)car).FuelConsumption}");
                    }

                }

            }

        }

        public void WriteOneCar(Car car)
        {
            if (car is Electric)
            {
                using (StreamWriter carWriter = new StreamWriter(this._filePath, true))
                {
                    carWriter.WriteLine($"{car.Id},{car.Maker},{car.Model},{car.RentPrice},{((Electric)car).BatteryCapacity},{((Electric)car).ChargeTime}");
                }
            }
            else
            {
                using (StreamWriter carWriter = new StreamWriter(this._filePath, true))
                {
                    carWriter.WriteLine($"{car.Id},{car.Maker},{car.Model},{car.RentPrice},{((Combustion)car).FuelConsumption}");
                }
            }

        }

        public void WriteOneCombustion(Combustion combustion)
        {
            throw new NotImplementedException();
        }

        public void WriteOneElectric(Electric electric)
        {
            throw new NotImplementedException();
        }
    }
}
