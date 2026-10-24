using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task.Context;
using Task.Models;
using Task.View_Model;

namespace Task.Controllers
{
    public class DepartmentController : Controller
    {
        DEPIContext db = new DEPIContext();
        public IActionResult ShowAll()
        {
            var result = db.Departments.ToList();
            return View("ShowAll", result);
        }
        public IActionResult ShowDetails(int id)
        {
            var result = db.Departments.FirstOrDefault(e => e.Id == id);
            return View("ShowDetails", result);
        }
        public IActionResult Add()
        {
            return View("Add");
        }
        public IActionResult Save(Department newDepartmentFromRequest)
        {
            if (newDepartmentFromRequest.Name != null)
            {
                db.Departments.Add(newDepartmentFromRequest);
                db.SaveChanges();
                return RedirectToAction("ShowAll");
            }
            return View("Add" , newDepartmentFromRequest);
        }

        public IActionResult ShowDepartmentInfo(int id)
        {
            var department = db.Departments
                .FirstOrDefault(d => d.Id == id);

            if (department == null)
                return NotFound();

            var students = db.Students
                .Where(s => s.DepartmentId == id && s.Age > 25)
                .ToList();

            var studentsCount = db.Students
                .Count(s => s.DepartmentId == id);

            var vm = new ShowStudentDepartmentViewModel
            {
                DepartmentId = department.Id,
                DepartmentName = department.Name,

                Students = students.Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList(),

                DepartmentState = studentsCount > 50
                    ? "Main"
                    : studentsCount < 50
                        ? "Branch"
                        : "Exactly 50 students"
            };

            return View("ShowDepartmentInfo", vm);
        }
    }
}
