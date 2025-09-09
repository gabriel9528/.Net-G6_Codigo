using ApplicationLayer.Interfaces;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeRepository.GetAllAsync();
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        [HttpGet("deleted")]
        public async Task<IActionResult> GetAllDeleted()
        {
            var employees = await _employeeRepository.GetAllDeleteAsync();
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            var response = await _employeeRepository.PostAsync(employee);
            return Ok(response);
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Employee employee)
        {
            var response = await _employeeRepository.EditAsync(employee);
            return Ok(response);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _employeeRepository.DeleteAsync(id);
            return Ok(response);
        }

    }
}
