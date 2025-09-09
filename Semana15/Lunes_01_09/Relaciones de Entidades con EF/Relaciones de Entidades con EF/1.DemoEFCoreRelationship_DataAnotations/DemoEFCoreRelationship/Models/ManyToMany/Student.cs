using System.Text.Json.Serialization;

namespace DemoEFCoreRelationship.Models.ManyToMany
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        //Relationship: many to many
        //[JsonIgnore]
        //public List<Subject>? Subjects { get; set; }

        // Relación correcta: Debe apuntar a StudentSubject
        public List<StudentSubject>? StudentSubjects { get; set; }

    }
}
