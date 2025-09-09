using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataNotations.Models.OneToOne
{
    public class CarModel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        [ForeignKey("CarCompany")]
        public int CarCompanyId { get; set; }
        public virtual CarCompany? CarCompany { get; set; }
    }
}
