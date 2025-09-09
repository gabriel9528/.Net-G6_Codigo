using DemoEFCoreRelationship.Data;
using DemoEFCoreRelationship.Models.OneToOne;
using Microsoft.EntityFrameworkCore;

namespace DemoEFCoreRelationship.Repo
{
    public class RepositoryOneToOne
    {
        private readonly AppDbContext _context;

        public RepositoryOneToOne(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarCompany>> GetCarCompanies() => 
            await _context.CarCompanies.ToListAsync();

        public async Task<List<CarModel>> GetCarModels() =>
            await _context.CarModels.ToListAsync();

        public async Task AddCarCompany(CarCompany carCompany)
        {
            _context.CarCompanies.Add(carCompany);
            await _context.SaveChangesAsync();
        }

        public async Task AddCarModel(CarModel carModel)
        {
            _context.CarModels.Add(carModel);
            await _context.SaveChangesAsync();
        }
        
    }
}
