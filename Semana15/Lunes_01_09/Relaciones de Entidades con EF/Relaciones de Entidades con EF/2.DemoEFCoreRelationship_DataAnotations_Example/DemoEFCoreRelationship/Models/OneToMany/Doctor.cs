using System.Text.Json.Serialization;

namespace DemoEFCoreRelationship.Models.OneToMany
{
    public class Doctor
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        //Relationship: one to many
        [JsonIgnore]
        public List<Patient>? Patients { get; set; }
    }
}
