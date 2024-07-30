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
        public DateOnly BirthDate { get; set; }


        public Customer(string name, string surname, DateOnly birthDate)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
        }
        public override string ToString()
        {
            return $"{Name} {Surname} {BirthDate}";
        }


    }
}
