using DemoEFCoreRelationship.Data;
using DemoEFCoreRelationship.Models.ManyToMany;
using DemoEFCoreRelationship.Models.OneToMany;
using DemoEFCoreRelationship.Models.OneToOne;
using Microsoft.EntityFrameworkCore;

namespace DemoEFCoreRelationship.Repo
{
    public class RepositoryManyToMany
    {
        private readonly AppDbContext _context;

        public RepositoryManyToMany(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }   

        public async Task<List<Student>> GetStudents() => 
            await _context.Students.Include(x=>x.StudentSubjects).ToListAsync();

        public async Task AddSubject(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Subject>> GetSubjects() => 
            await _context.Subjects.Include(x=>x.StudentSubjects).ToListAsync();

        public async Task AddStudentSubject(int studentId, int subjectId)
        {
            var student = await _context.Students.FindAsync(studentId);
            var subject = await _context.Subjects.FindAsync(subjectId);

            if (student != null && subject != null)
            {
                var studentSubject = new StudentSubject
                {
                    StudentId = studentId,
                    SubjectId = subjectId,
                    Student = student,
                    Subject = subject
                };

                _context.StudentSubject.Add(studentSubject);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<StudentSubject>> GetAllStudentSubjects()
        {
            return await _context.StudentSubject
                .Include(x => x.Student)
                .Include(x => x.Subject)
                .ToListAsync();
        }


    }
}
