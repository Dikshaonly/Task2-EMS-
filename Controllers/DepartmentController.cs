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

        public IActionResult Edit(int Id){
            var data = _context.Departments.Find(Id);
            if(data==null){
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Department dep){
            if (ModelState.IsValid)
            {
                _context.Departments.Update(dep);
            _context.SaveChanges();
                return RedirectToAction("Index","Department");
            }
            return View(dep);

        }

        public IActionResult Delete(int Id){
            try{
                var data = _context.Departments.Find(Id);
                if(data==null){
                return NotFound();
            }
            var hasEmployee = _context.Departments.Any(e=>e.DepId==Id);
            if(hasEmployee){
                TempData["ErrorMessage"] = "Cannot delete this department because it has employees assigned to it. Please reassign or delete the employees first.";
                return RedirectToAction("Index","Department");
            }
                _context.Departments.Remove(data);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Department deleted successfully";
                return RedirectToAction("Index","Department");
            }
            catch(Exception ex){
                TempData["ErrorMessage"] = "Error occured while deleting department"+ex.Message;
                return RedirectToAction("Index","Department");
            }
        }

        
    }
 }