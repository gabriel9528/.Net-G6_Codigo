using System.ComponentModel.DataAnnotations.Schema;

namespace DemoEFCoreRelationship.Models.OneToOne
{
    public class EmployeeAddress
    {
        public int Id { get; set; }

        public string? Street { get; set; }

        public string? City { get; set; }

        // Relación: uno a uno
        // Especificas que esta propiedad es la clave foránea
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; } // Clave foránea

        public Employee? Employee { get; set; } // Propiedad de navegación
    }
}
