using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DemoEFCoreRelationship.Models.ManyToMany
{
    [PrimaryKey(nameof(StudentId), nameof(SubjectId))] // Definir clave compuesta correctamente
    public class StudentSubject
    {
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}
