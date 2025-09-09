using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataNotations.Models.OneToMany
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]
        public List<Patient> Patients { get; set; }
    }
}
