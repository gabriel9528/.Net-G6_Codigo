using DemoEFCoreRelationship.Data;
using DemoEFCoreRelationship.Models.ManyToMany;
using DemoEFCoreRelationship.Models.OneToMany;
using DemoEFCoreRelationship.Models.OneToOne;
using Microsoft.EntityFrameworkCore;

namespace DemoEFCoreRelationship.Repo
{
    public class RepositoryPersonBusiness
    {
        private readonly AppDbContext _context;

        public RepositoryPersonBusiness(AppDbContext context)
        {
            _context = context;
        }

        // Agregar una nueva persona
        public async Task AddPerson(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }

        // Obtener todas las personas con sus empresas asociadas
        public async Task<List<Person>> GetPeople() =>
            await _context.Persons.Include(p => p.Businesses).ToListAsync();

        // Agregar una nueva empresa
        public async Task AddBusiness(Business business)
        {
            _context.Businesses.Add(business);
            await _context.SaveChangesAsync();
        }

        // Obtener todas las empresas con sus personas asociadas
        public async Task<List<Business>> GetBusinesses() =>
            await _context.Businesses.Include(b => b.People).ToListAsync();
    }
}
