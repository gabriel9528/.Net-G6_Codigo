using DemoEFCoreRelationship.Data;
using DemoEFCoreRelationship.Models.OneToMany;
using DemoEFCoreRelationship.Models.OneToOne;
using Microsoft.EntityFrameworkCore;

namespace DemoEFCoreRelationship.Repo
{
    public class RepositoryOneToMany
    {
        private readonly AppDbContext _context;

        public RepositoryOneToMany(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Patient>> GetPatients() => 
            await _context.Patients.Include(x=>x.Doctor).ToListAsync();



        public async Task<List<Doctor>> GetDoctors() => 
            await _context.Doctors.ToListAsync();

        public async Task AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        // New methods for Author and Book

        public async Task AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Author>> GetAuthors() =>
            await _context.Authors.Include(x => x.Books).ToListAsync();

        public async Task AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Book>> GetBooks() =>
            await _context.Books.Include(x => x.Author).ToListAsync();


    }
}
