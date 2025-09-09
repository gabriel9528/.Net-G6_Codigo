namespace DemoEFCoreRelationship.Models.OneToOne
{
    public class Employee
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        // Relación: uno a uno
        public EmployeeAddress? EmployeeAddress { get; set; } // Propiedad de navegación
    }
}
