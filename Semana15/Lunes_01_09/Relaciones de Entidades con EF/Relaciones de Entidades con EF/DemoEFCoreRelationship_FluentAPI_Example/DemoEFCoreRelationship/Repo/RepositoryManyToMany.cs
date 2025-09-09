using DemoEFCoreRelationship.Data;
using DemoEFCoreRelationship.Models.ManyToMany.Exercise1;
using DemoEFCoreRelationship.Models.ManyToMany.Exercise2;
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

        public async Task AddStudentSubject(Test test)
        {
            await _context.StudentSubjects.AddAsync(new StudentSubject()
            {
                StudentId = test.StudentId,
                SubjectId = test.SubjectId
            });
            await _context.SaveChangesAsync();
        }

        public async Task<List<StudentSubject>> GetStudentSubjects() => 
            await _context.StudentSubjects
            .Include(s => s.Student)
            .Include(s => s.Subject)
            .ToListAsync();

        #region PersonBusiness
        // Person and Business Methods

        // Add a new Person
        public async Task AddPerson(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }

        // Get all Persons with their associated Businesses
        public async Task<List<Person>> GetPersons() =>
            await _context.Persons.Include(p => p.PersonBusinesses)
                                  .ThenInclude(pb => pb.Business)
                                  .ToListAsync();

        // Add a new Business
        public async Task AddBusiness(Business business)
        {
            _context.Businesses.Add(business);
            await _context.SaveChangesAsync();
        }

        // Get all Businesses with their associated Persons
        public async Task<List<Business>> GetBusinesses() =>
            await _context.Businesses.Include(b => b.PersonBusinesses)
                                     .ThenInclude(pb => pb.Person)
                                     .ToListAsync();

        // Add a PersonBusiness relationship
        public async Task AddPersonBusiness(Test2 test2)
        {
            var personBusiness = new PersonBusiness
            {
                PersonId = test2.PersonId,
                BusinessId = test2.BusinessId
            };

            _context.PersonBusinesses.Add(personBusiness);
            await _context.SaveChangesAsync();
        }

        // Get all PersonBusiness relationships
        public async Task<List<PersonBusiness>> GetPersonBusinesses() =>
            await _context.PersonBusinesses
                          .Include(pb => pb.Person)
                          .Include(pb => pb.Business)
                          .ToListAsync();
        #endregion

    }
}
