﻿using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            List<Seller> sellers = _sellerService.FindAll();
            
            return View(sellers);
        }

        public IActionResult Create()
        {
            //return View();
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {

            if (id  == null)
            {
                return NotFound();
            }

            Seller seller = _sellerService.FindById(id.Value);
                                
            return View(seller);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {            
            _sellerService.Delete(id);

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Seller seller = _sellerService.FindById(id.Value);

            return View(seller);
        }
    }
}
