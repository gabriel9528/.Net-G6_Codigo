using ApplicationLayer.Dtos;
using ApplicationLayer.Interfaces;
using DomainLayer.Entities;
using InfraestructureLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace InfraestructureLayer.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<ServiceResponse> PostAsync(Employee employee)
        {
            var employeeExists = await _context.Employees
                .FirstOrDefaultAsync(x => x.Name.ToLower() == employee.Name.ToLower() && x.IsActive);

            if (employeeExists != null)
            {
                return new ServiceResponse(false, "Employee already exists");
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return new ServiceResponse(true, "Employee add successfully");

        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var employeeExists = await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == id & x.IsActive);

            if (employeeExists != null)
            {
                employeeExists.IsActive = false;
                await _context.SaveChangesAsync();
                return new ServiceResponse(true, "Employee deleted successfully");
            }
            return new ServiceResponse(false, "Employee doesn't exists");
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees.AsNoTracking().Where(x => x.IsActive).ToListAsync();
        }

        public async Task<List<Employee>> GetAllDeleteAsync()
        {
            return await _context.Employees.AsNoTracking().Where(x => !x.IsActive).ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var employeeExists = await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

            return employeeExists != null ? employeeExists : new Employee();
        }

        public async Task<ServiceResponse> EditAsync(Employee employee)
        {
            _context.Update(employee);
            await _context.SaveChangesAsync();
            return new ServiceResponse(true, "Employee update succesfully");
        }
    }
}
