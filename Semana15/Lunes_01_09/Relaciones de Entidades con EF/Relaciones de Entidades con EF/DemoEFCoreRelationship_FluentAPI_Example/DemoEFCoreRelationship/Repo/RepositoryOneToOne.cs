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
            await _context.CarCompanies.Include(x => x.CarModel).ToListAsync();

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
        public async Task<List<CarModel>> GetCarModels() => 
            await _context.CarModels.Include(a => a.CarCompany).ToListAsync();

        // Employee - EmployeeAddress (Nuevo)
        public async Task<List<Employee>> GetEmployees()
        {
            return await _context.Employees.Include(e => e.EmployeeAddress).ToListAsync();
        }

        public async Task AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task AddEmployeeAddress(EmployeeAddress employeeAddress)
        {
            _context.EmployeeAddresses.Add(employeeAddress);
            await _context.SaveChangesAsync();
        }

        public async Task<List<EmployeeAddress>> GetEmployeeAddresses()
        {
            return await _context.EmployeeAddresses.Include(ea => ea.Employee).ToListAsync();
        }
    }
}
