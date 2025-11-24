using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Task2.Data.Models;
 namespace Task2.Controllers
 {
    public class EmployeeController:Controller{
        private readonly MyDbContext _context;
        public EmployeeController(MyDbContext context){
            _context = context;
        }
        public IActionResult Index(){
            var data = _context.Employees.ToList();
            return View(data);
        }
        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee emp){
            if (ModelState.IsValid)
            {
                _context.Employees.Add(emp);
                _context.SaveChanges();
                return RedirectToAction("Index","Employee");
            }
            return View(emp);
        }
    }
 }