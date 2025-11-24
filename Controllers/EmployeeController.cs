using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            ViewBag.Departments = new SelectList(_context.Departments, "DepId", "DepName");
            ViewBag.Designations = new SelectList(_context.Designations, "Did", "Dname"); 
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
            ViewBag.Departments = new SelectList(_context.Departments, "DepId", "DepName");
            ViewBag.Designations = new SelectList(_context.Designations, "Did", "Dname");
            return View(emp);
        }
    }
 }