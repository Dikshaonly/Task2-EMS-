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
    }
 }