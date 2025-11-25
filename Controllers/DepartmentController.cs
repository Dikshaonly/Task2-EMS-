using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Task2.Data.Models;
 namespace Task2.Controllers{
    public class DepartmentController:Controller{
        private readonly MyDbContext _context;
        public DepartmentController(MyDbContext context){
            _context=context;
        }
        
        public IActionResult Index(){
            var data=_context.Departments.ToList();
            return View(data);
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department dep){
            if (ModelState.IsValid)
            {
                _context.Departments.Add(dep);
                _context.SaveChanges();
                return RedirectToAction("Index","Department");
            }
            return View(dep);
        } 
    
    }
 }