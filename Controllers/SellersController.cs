using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models.Services.Exceptions;
using System.Diagnostics;

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
        public async Task<IActionResult> Index()
        {
            List<Seller> sellers = await _sellerService.FindAllAsync();
            
            return View(sellers);
        }

        public async Task<IActionResult> Create()
        {
            //return View();
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {

            if (!ModelState.IsValid)
            {
                List<Department> departaments = await _departmentService.FindAllAsync();
                SellerFormViewModel viewModel = new SellerFormViewModel { Departments = departaments, Seller = seller };

                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id  == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            Seller seller = await _sellerService.FindByIdAsync(id.Value);
                                
            return View(seller);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try { 
                await _sellerService.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            Seller seller = await _sellerService.FindByIdAsync(id.Value);

            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            Seller seller = await _sellerService.FindByIdAsync(id.Value);            
            if(seller == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

            return View(viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                List<Department> departaments = await _departmentService.FindAllAsync();
                SellerFormViewModel viewModel = new SellerFormViewModel { Departments = departaments, Seller = seller };

                return View(viewModel);
            }
            
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            
        }

        public IActionResult Error(string message)
        {
            ErrorViewModel viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
