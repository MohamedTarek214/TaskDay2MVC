using Task.Models;

namespace Task.ViewModel
{
    public class StudentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int DepartmentId { get; set; }

        public List<Department> DeptList { get; set; }
    }
}

