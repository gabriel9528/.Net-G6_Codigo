using System.Text.Json.Serialization;

namespace DemoEFCoreRelationship.Models.ManyToMany2
{
    public class Business
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Relación: muchos a muchos
        [JsonIgnore]
        public List<Person>? Persons { get; set; }
    }
}
