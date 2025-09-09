using System.Text.Json.Serialization;

namespace FluentAPI.Models.OneToMany
{
    public class Doctor
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]
        public List<Patient>? Patients { get; set; }
    }
}
