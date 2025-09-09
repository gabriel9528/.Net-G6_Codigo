using System.Text.Json.Serialization;

namespace FluentAPI.Models.ManyToMany
{
    public class Course
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
