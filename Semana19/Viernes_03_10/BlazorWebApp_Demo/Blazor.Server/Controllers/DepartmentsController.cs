using Blazor.Server.Data;
using Blazor.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            var listDepartments = await _context.Departments.ToListAsync();
            return listDepartments;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if(department == null)
            {
                return NotFound();
            }
            return department;
        }

        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartment", new { id = department.Id }, department);
        }
    }
}
