using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public ICollection<Saller> Sallers = new List<Saller>();



        public Department()
        {
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSaller(Saller saller)
        {
            Sallers.Add(saller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sallers.Sum(saller => saller.TotalSales(initial, final));
        }
    }
}
