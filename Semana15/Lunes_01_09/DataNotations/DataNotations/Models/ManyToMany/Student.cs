using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataNotations.Models.ManyToMany
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]
        public List<StudentCourse>? StudentCourses { get; set; }
    }
}
