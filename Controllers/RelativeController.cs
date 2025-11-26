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

    
    }
 }