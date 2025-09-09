using System.Text.Json.Serialization;

namespace FluentAPI.Models.ManyToMany
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
