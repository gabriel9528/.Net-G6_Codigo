using System.ComponentModel.DataAnnotations;

namespace Blazor.Shared
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El salario del empleado es obligatorio")]
        [Range(0, int.MaxValue)]
        public string Salary { get; set; }

        public DateOnly DateContract { get; set; }

        public int IdDepartment { get; set; }
        public DepartmentDto? Department { get; set; }
    }
}
