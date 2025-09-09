using DemoEFCoreRelationship.Models.ManyToMany.Exercise1;
using System.Text.Json.Serialization;

namespace DemoEFCoreRelationship.Models.ManyToMany.Exercise2
{
    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Relación: muchos a muchos
        [JsonIgnore]
        public ICollection<PersonBusiness>? PersonBusinesses { get; set; }
    }
}
