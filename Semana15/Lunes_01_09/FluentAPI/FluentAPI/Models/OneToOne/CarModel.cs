using System.ComponentModel.DataAnnotations.Schema;

namespace FluentAPI.Models.OneToOne
{
    public class CarModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int CarCompanyId { get; set; }
        public virtual CarCompany? CarCompany { get; set; }
    }
}
