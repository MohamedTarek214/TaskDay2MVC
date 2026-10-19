using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task.Context;
using Task.Models;

namespace Task.Controllers
{
    public class StudentController : Controller
    {
        DEPIContext db = new DEPIContext();
        public IActionResult ShowAll()
        {
            var result = db.Students.Include(e => e.Department).ToList();
            return View("ShowAll" , result);
        }
        public IActionResult ShowDetails(int id)
        {
            var result = db.Students.Include(e => e.Department)
                .FirstOrDefault(e => e.Id == id);
            
            return View("ShowDetails", result);
        }
    }
}
