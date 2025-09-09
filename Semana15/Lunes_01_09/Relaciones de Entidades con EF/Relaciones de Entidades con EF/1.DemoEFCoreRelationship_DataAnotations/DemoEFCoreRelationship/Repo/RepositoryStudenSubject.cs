using DemoEFCoreRelationship.Data;
using DemoEFCoreRelationship.Models.ManyToMany;
using DemoEFCoreRelationship.Models.OneToMany;
using DemoEFCoreRelationship.Models.OneToOne;
using Microsoft.EntityFrameworkCore;

namespace DemoEFCoreRelationship.Repo
{
    public class RepositoryStudenSubject
    {
        private readonly AppDbContext _context;

        public RepositoryStudenSubject(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }   

        //public async Task<List<Student>> GetStudents() => 
        //    await _context.Students.Include(x=>x.Subjects).ToListAsync();

        public async Task AddSubject(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
        }

        //public async Task<List<Subject>> GetSubjects() => 
        //    await _context.Subjects.Include(x=>x.Students).ToListAsync();

    }
}
