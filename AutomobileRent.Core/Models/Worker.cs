using AutomobileRent.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileRent.Core.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public WorkerPosition Position { get; set; }

        public Worker() { }

        public override string ToString()
        {
            return $"Woker Id: {Id}, {Name} {Surname}, Position: {Position}";
        }

    }
}
