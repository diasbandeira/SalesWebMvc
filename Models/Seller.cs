﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> SalesRecords { get; set; } = new List<SalesRecord>();



        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }
        
        public void AddSeller(SalesRecord seller)
        {
            SalesRecords.Add(seller);
        }

        public void Remove(SalesRecord seller)
        {
            SalesRecords.Remove(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return SalesRecords.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }



    }
}
