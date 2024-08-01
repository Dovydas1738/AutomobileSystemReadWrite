using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileRent.Core.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int CustomerId { get; set; }


        public Customer(string name, string surname, DateTime birthDate)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
        }

        //public Customer(int customerId, string name, string surname, DateTime birthDate)
        //{
        //    Name = name;
        //    Surname = surname;
        //    BirthDate = birthDate;
        //    CustomerId = customerId;
        //}


        public Customer() { }

        public override string ToString()
        {
            return $"{Name} {Surname} {BirthDate}";
        }


    }
}
