using System.Text.Json.Serialization;

namespace DemoEFCoreRelationship.Models.OneToMany
{
    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Relación: uno a muchos
        [JsonIgnore]
        public List<Book>? Books { get; set; }
    }
}
