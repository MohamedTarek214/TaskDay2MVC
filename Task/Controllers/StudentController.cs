using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task.Context;
using Task.Models;
using Task.ViewModel;

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
            if (result == null)
            {
                return NotFound();
            }
            return View("ShowDetails", result);
        }
        public IActionResult Add()
        {
            StudentVM vm = new StudentVM();
            vm.DeptList = db.Departments.ToList();

            return View("Add", vm);
        }
        [HttpPost]
        public IActionResult Save(StudentVM newStdFromRequest)
        {
            if (newStdFromRequest.Name != null &&
                db.Departments.Any(e => e.Id == newStdFromRequest.DepartmentId))
            {
                Student student = new Student();

                student.Name = newStdFromRequest.Name;
                student.Age = newStdFromRequest.Age;
                student.DepartmentId = newStdFromRequest.DepartmentId;

                db.Students.Add(student);
                db.SaveChanges();

                return RedirectToAction("ShowAll");
            }

            newStdFromRequest.DeptList = db.Departments.ToList();
            return View("Add", newStdFromRequest);
        }

        public IActionResult Edit(int id)
        {
            var result = db.Students.FirstOrDefault(e => e.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            StudentVM vm = new StudentVM();

            vm.Id = result.Id;
            vm.Name = result.Name;
            vm.Age = result.Age;
            vm.DepartmentId = result.DepartmentId;
            vm.DeptList = db.Departments.ToList();

            return View("Edit", vm);
        }
        [HttpPost]
        public IActionResult SaveEdit(StudentVM newStdFromRequest)
        {
            var student = db.Students.FirstOrDefault(e => e.Id == newStdFromRequest.Id);

            if (student == null)
            {
                return NotFound();
            }

            if (newStdFromRequest.Name != null &&
                db.Departments.Any(e => e.Id == newStdFromRequest.DepartmentId))
            {
                student.Name = newStdFromRequest.Name;
                student.Age = newStdFromRequest.Age;
                student.DepartmentId = newStdFromRequest.DepartmentId;

                db.SaveChanges();

                return RedirectToAction("ShowAll");
            }

            newStdFromRequest.DeptList = db.Departments.ToList();
            return View("Edit", newStdFromRequest);
        }

        // Delete - Show Warning
        public IActionResult Delete(int id)
        {
            var result = db.Students
                .Include(e => e.Department)
                .FirstOrDefault(e => e.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return View("Delete", result);
        }

        // Delete - Confirm
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = db.Students.FirstOrDefault(e => e.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            db.Students.Remove(result);
            db.SaveChanges();

            return RedirectToAction("ShowAll");
        }
    }

}

