using System.Text.Json.Serialization;

namespace DemoEFCoreRelationship.Models.ManyToMany2
{
    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Relación: muchos a muchos
        [JsonIgnore]
        public List<Business>? Businesses { get; set; }
    }
}
