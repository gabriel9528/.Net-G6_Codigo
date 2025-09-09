using System.ComponentModel.DataAnnotations.Schema;

namespace DemoEFCoreRelationship.Models.OneToOne
{
    public class CarModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        //Relationship: one to one
        // Especificas que esta propiedad es la clave foránea
        //[ForeignKey("CarCompany")]
        public int CarCompanyId { get; set; } //Foreign key
        public CarCompany? CarCompany { get; set; } //Navigation property
    }
}
