using Blazor.Server.Data;
using Blazor.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var listEmpployees = await _context.Employees.Include(x => x.Department).ToListAsync();
            return Ok(listEmpployees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            var departmentExists = await _context.Departments.AnyAsync(x => x.Id == employee.IdDepartment);
            if (!departmentExists)
            {
                return BadRequest("El departamento especificado no existe");
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var createdEmployee = 
                await _context.Employees.Include(x =>x.Department).FirstOrDefaultAsync(x => x.Id == employee.Id);

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, createdEmployee);
        }
    }
}
