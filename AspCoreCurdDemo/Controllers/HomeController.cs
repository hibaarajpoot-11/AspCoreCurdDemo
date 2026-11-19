using Microsoft.EntityFrameworkCore;
using AspCoreCurdDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AspCoreCurdDemo.Data;

namespace AspCoreCurdDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _context;

        public HomeController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.TotalEnrollments = _context.Students.Count();
            int totalStudents = _context.Students.Count();
            int totalCourses = _context.Students
         .Select(s => s.Course)
         .Distinct()
            .Count();

            ViewBag.TotalCourses = totalCourses;

            ViewBag.TotalStudents = totalStudents;
            var recentStudents = _context.Students
    .OrderByDescending(s => s.StudentId)
    .Take(3)
    .ToList();

            ViewBag.RecentStudents = recentStudents;

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
}
