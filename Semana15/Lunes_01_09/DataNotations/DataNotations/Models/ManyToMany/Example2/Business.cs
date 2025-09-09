using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataNotations.Models.ManyToMany.Example2
{
    public class Business
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]
        public List<Person>? People { get; set; }
    }
}
