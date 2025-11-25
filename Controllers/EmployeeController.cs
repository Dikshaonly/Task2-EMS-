using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var data = _context.Employees
            .Include(e=>e.Dep)
            .Include(e=>e.DidNavigation)
            .ToList();
            Console.WriteLine(data);
            return View(data);
        }
        public IActionResult Create(){
            ViewBag.Departments = new SelectList(_context.Departments, "DepId", "DepName");
            ViewBag.Designations = new SelectList(_context.Designations, "Did", "Dname"); 
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee emp){
             ModelState.Remove("Dep");
            ModelState.Remove("DidNavigation");
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

        public IActionResult Edit(int Id){
            ViewBag.Departments = new SelectList(_context.Departments, "DepId", "DepName");
            ViewBag.Designations = new SelectList(_context.Designations, "Did", "Dname");
            var data = _context.Employees.Find(Id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp){
             ModelState.Remove("Dep");
            ModelState.Remove("DidNavigation");
            if (ModelState.IsValid)
            {
                _context.Employees.Update(emp);
                _context.SaveChanges();
                return RedirectToAction("Index","Employee");
            }
             ViewBag.Departments = new SelectList(_context.Departments, "DepId", "DepName");
            ViewBag.Designations = new SelectList(_context.Designations, "Did", "Dname");
            return View(emp);
        }

        public IActionResult Delete(int id){
            var data = _context.Employees.Find(id);
            if(data==null){
                return NotFound();
            }
            _context.Employees.Remove(data);
                _context.SaveChanges();
                return RedirectToAction("Index","Employee");
            ViewBag.Departments = new SelectList(_context.Departments, "DepId", "DepName");
            ViewBag.Designations = new SelectList(_context.Designations, "Did", "Dname");

        }

        /*[HttpPost]
        public IActionResult Delete(Employee emp){
            ModelState.Remove("Dep");
            ModelState.Remove("DidNavigation");
            if (ModelState.IsValid)
            {
                _context.Employees.Remove(emp);
                _context.SaveChanges();
                return RedirectToAction("Index","Employee");
            }
            return View(emp);
        }*/
    }
 }