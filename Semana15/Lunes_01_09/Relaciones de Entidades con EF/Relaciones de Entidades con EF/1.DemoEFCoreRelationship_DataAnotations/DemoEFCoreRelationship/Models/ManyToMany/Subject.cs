using System.Text.Json.Serialization;

namespace DemoEFCoreRelationship.Models.ManyToMany
{
    public class Subject
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        //Relationship: many to many
        //[JsonIgnore]
        //public List<Student>? Students { get; set; }

        // Relación correcta: Debe apuntar a StudentSubject
        public List<StudentSubject>? StudentSubjects { get; set; }
    }
}
