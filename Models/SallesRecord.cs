using SalesWebMvc.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public class SallesRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public SaleStatus SalesStatus { get; set; }
        public Saller Saller { get; set; }

        public SallesRecord()
        {

        }
        
        public SallesRecord(int id, DateTime date, double amount, SaleStatus salesStatus, Saller saller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            SalesStatus = salesStatus;
            Saller = saller;
        }
    }
}
