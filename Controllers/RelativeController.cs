using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Task2.Data.Models;
 namespace Task2.Controllers
 {
    public class RelativeController:Controller{
        private readonly MyDbContext _context;
        public RelativeController(MyDbContext context){
            _context=context;
        }
        public IActionResult Index(int empId){
            var data = _context.Employees.Find(empId);
            if(data==null){
                return NotFound();
            }
            ViewBag.EName=data.Name;
            ViewBag.eId=empId;
            var relatives=_context.Relatives.Where(r=>r.Eid==empId).ToList();
            return View(relatives);
        }

        public IActionResult Create(int id){
            var data = _context.Employees.Find(id);
            if(data==null){
                return NotFound();
            }
            ViewBag.EName =data.Name;
            ViewBag.eId = id;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Relative rel,int id){
            ModelState.Remove("EidNavigation");
            if (ModelState.IsValid)
            {
                rel.Eid=id;
                _context.Relatives.Add(rel);
                _context.SaveChanges();
                return RedirectToAction("Details","Employee",new{Id=id});
            }
           var data = _context.Employees.Find(id);
           ViewBag.EName =data?.Name;
            ViewBag.eId = id;
            return View(rel);
        }

        public IActionResult Edit(int id){
            var data = _context.Relatives
                .Include(r => r.EidNavigation)
                .FirstOrDefault(r => r.Rid == id);
                
            if (data == null)
            {
                return NotFound();
            }
            
            ViewBag.EName = data.EidNavigation?.Name;
            ViewBag.eId = data.Eid;
            
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Relative rel){
            ModelState.Remove("EidNavigation");
            if (ModelState.IsValid)
            {
                _context.Relatives.Update(rel);
                _context.SaveChanges();
                return RedirectToAction("Details","Employee",new{Id=rel.Eid});
            }
           var data = _context.Employees.Find(rel.Eid);
           ViewBag.EName =data?.Name;
            ViewBag.eId = rel.Eid;
            return View(rel);
        }

         public IActionResult Delete(int id)
        {
            try
            {
                var data = _context.Relatives.Find(id);
                if (data == null)
                {
                    return NotFound();
                }
                
                int employeeId = data.Eid ?? 0;
                
                _context.Relatives.Remove(data);
                _context.SaveChanges();
                
                TempData["SuccessMessage"] = "Relative deleted successfully";
                return RedirectToAction("Details", "Employee", new { Id = employeeId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error occurred while deleting relative: " + ex.Message;
                return RedirectToAction("Index", "Employee");
            }
        }
    }
 }