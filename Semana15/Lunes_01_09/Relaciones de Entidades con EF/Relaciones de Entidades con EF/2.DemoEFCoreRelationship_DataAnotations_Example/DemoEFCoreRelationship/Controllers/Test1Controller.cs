using DemoEFCoreRelationship.Models.ManyToMany;
using DemoEFCoreRelationship.Models.OneToMany;
using DemoEFCoreRelationship.Models.OneToOne;
using DemoEFCoreRelationship.Repo;
using DemoEFCoreRelationship.Repo.OneToMany;
using Microsoft.AspNetCore.Mvc;

namespace DemoEFCoreRelationship.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Test1Controller : ControllerBase
    {
        private readonly RepositoryEmployeeEmployeeAddress _repositoryEmployeeEmployeeAddress;
        private readonly RepositoryAuthorBook _repositoryAuthorBook;
        private readonly RepositoryPersonBusiness _repositoryPersonBusiness;

        public Test1Controller(RepositoryEmployeeEmployeeAddress repositoryEmployeeEmployeeAddress,
            RepositoryAuthorBook repositoryAuthorBook,
            RepositoryPersonBusiness repositoryPersonBusiness)
        {
            _repositoryEmployeeEmployeeAddress = repositoryEmployeeEmployeeAddress;
            _repositoryAuthorBook = repositoryAuthorBook;
            _repositoryPersonBusiness = repositoryPersonBusiness;
        }

        //One to one
        // One to One: Obtener todos los empleados con sus direcciones
        [HttpGet("GetEmployeesWithAddresses")]
        public async Task<ActionResult<List<Employee>>> GetEmployeesWithAddresses()
        {
            var employees = await _repositoryEmployeeEmployeeAddress.GetEmployees();
            return Ok(employees);
        }

        // One to One: Agregar un nuevo empleado
        [HttpPost("AddEmployee")]
        public async Task<ActionResult> AddEmployee(Employee employee)
        {
            await _repositoryEmployeeEmployeeAddress.AddEmployee(employee);
            return Ok();
        }

        // One to One: Agregar una nueva dirección de empleado
        [HttpPost("AddEmployeeAddress")]
        public async Task<ActionResult> AddEmployeeAddress(EmployeeAddress employeeAddress)
        {
            await _repositoryEmployeeEmployeeAddress.AddEmployeeAddress(employeeAddress);
            return Ok();
        }

        //one to many
        // One to Many: Obtener todos los autores con sus libros
        [HttpGet("GetAuthorsWithBooks")]
        public async Task<ActionResult<List<Author>>> GetAuthorsWithBooks()
        {
            var authors = await _repositoryAuthorBook.GetAuthorsWithBooks();
            return Ok(authors);
        }

        // One to Many: Agregar un nuevo autor
        [HttpPost("AddAuthor")]
        public async Task<ActionResult> AddAuthor(Author author)
        {
            await _repositoryAuthorBook.AddAuthor(author);
            return Ok();
        }

        // One to Many: Agregar un nuevo libro
        [HttpPost("AddBook")]
        public async Task<ActionResult> AddBook(Book book)
        {
            await _repositoryAuthorBook.AddBook(book);
            return Ok();
        }

        // One to Many: Obtener todos los libros
        [HttpGet("GetBooks")]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var books = await _repositoryAuthorBook.GetBooks();
            return Ok(books);
        }

        //Many to many
        // Many to Many: Obtener todas las personas con sus empresas asociadas
        [HttpGet("GetPeopleWithBusinesses")]
        public async Task<ActionResult<List<Person>>> GetPeopleWithBusinesses()
        {
            var people = await _repositoryPersonBusiness.GetPeople();
            return Ok(people);
        }

        // Many to Many: Agregar una nueva persona
        [HttpPost("AddPerson")]
        public async Task<ActionResult> AddPerson(Person person)
        {
            await _repositoryPersonBusiness.AddPerson(person);
            return Ok();
        }

        // Many to Many: Agregar una nueva empresa
        [HttpPost("AddBusiness")]
        public async Task<ActionResult> AddBusiness(Business business)
        {
            await _repositoryPersonBusiness.AddBusiness(business);
            return Ok();
        }

        // Many to Many: Obtener todas las empresas con sus personas asociadas
        [HttpGet("GetBusinessesWithPeople")]
        public async Task<ActionResult<List<Business>>> GetBusinessesWithPeople()
        {
            var businesses = await _repositoryPersonBusiness.GetBusinesses();
            return Ok(businesses);
        }


    }
}
