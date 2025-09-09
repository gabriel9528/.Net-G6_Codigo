using DemoEFCoreRelationship.Data;
using DemoEFCoreRelationship.Models.OneToMany;
using Microsoft.EntityFrameworkCore;

namespace DemoEFCoreRelationship.Repo.OneToMany
{
    public class RepositoryAuthorBook
    {
        private readonly AppDbContext _context;

        public RepositoryAuthorBook(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los autores junto con sus libros
        public async Task<List<Author>> GetAuthorsWithBooks() =>
            await _context.Authors.Include(a => a.Books).ToListAsync();

        // Agregar un nuevo autor
        public async Task AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        // Agregar un nuevo libro
        public async Task AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        // Obtener todos los libros
        public async Task<List<Book>> GetBooks() =>
            await _context.Books.Include(b => b.Author).ToListAsync();
    }
}
