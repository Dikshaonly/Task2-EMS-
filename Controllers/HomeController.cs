using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Task2.Models;
using Task2.Data.Models;

namespace Task2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MyDbContext _context;

    public HomeController(ILogger<HomeController> logger, MyDbContext context)
    {
        _logger = logger;
        _context=context;
    }

    public IActionResult Index()
    {
        var totalEmployees =_context.Employees.Count();

        var withRelatives = _context.Employees
        .Include(e=>e.Relatives)
        .Where(e=>e.Relatives.Any())
        .Count();

        var withoutRelatives = totalEmployees - withRelatives;

        var withRelativesPercentage = totalEmployees > 0 
            ? Math.Round((double)withRelatives / totalEmployees * 100, 1) 
            : 0;

        var withoutRelativesPercentage = totalEmployees > 0 
        ? Math.Round((double)withoutRelatives/totalEmployees * 100, 1)
        :0;
         
         //group by gender
          var employeesByGender = _context.
            .GroupBy(e => string.IsNullOrEmpty(e.Gender) ? "Not Specified" : e.Gender)
            .Select(g => new { 
                Gender = g.Key, 
                Count = g.Count(),
                Percentage = totalEmployees > 0 ? Math.Round((double)g.Count() / totalEmployees * 100, 1) : 0
            })
            .ToList();
         
         ViewBag.TotalEmployee = totalEmployees;
         ViewBag.WithRelatives = withRelatives;
         ViewBag.WithoutRelatives = withoutRelatives;
         ViewBag.WithRelativesPercentage = withRelativesPercentage;
         ViewBag.WithoutRelativesPercentage = withoutRelativesPercentage;
         ViewBag.EmployeesByGender = employeesByGender;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
