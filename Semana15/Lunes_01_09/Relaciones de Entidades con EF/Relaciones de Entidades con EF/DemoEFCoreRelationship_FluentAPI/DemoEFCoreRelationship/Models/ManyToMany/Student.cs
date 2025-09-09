using System.Text.Json.Serialization;

namespace DemoEFCoreRelationship.Models.ManyToMany
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        //Relationship: many to many
        [JsonIgnore]
        public ICollection<StudentSubject>? StudentSubjects { get; set; }

    }
}
