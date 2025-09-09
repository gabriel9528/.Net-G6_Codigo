using Microsoft.EntityFrameworkCore;

namespace DataNotations.Models.ManyToMany
{
    [PrimaryKey(nameof(StudentId), nameof(CourseId))]
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
