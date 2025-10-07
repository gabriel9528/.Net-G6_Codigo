using System.ComponentModel.DataAnnotations;

namespace Blazor.Server.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public DateOnly DateContract { get; set; }
        public int IdDepartment { get; set; }
        public virtual Department? Department { get; set; }
    }
}
