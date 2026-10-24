using Microsoft.AspNetCore.Mvc.Rendering;

namespace Task.View_Model
{
    public class ShowStudentDepartmentViewModel
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public List<SelectListItem> Students { get; set; } = new();

        public string DepartmentState { get; set; } = string.Empty;
    }
}
