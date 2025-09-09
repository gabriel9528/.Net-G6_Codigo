using System.Text.Json.Serialization;

namespace DemoEFCoreRelationship.Models.ManyToMany
{
    public class Business
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Relación: muchos a muchos
        [JsonIgnore]
        public List<Person>? People { get; set; }
    }
}
