using System.ComponentModel.DataAnnotations;

namespace Task.Models
{
    public class Department
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string? MangerName { get; set; }
        public ICollection<Student> Students { get; set; } 
        public ICollection<Teacher> Teachers { get; set; } 
        public ICollection<Course> Courses { get; set; } 
    }
}
