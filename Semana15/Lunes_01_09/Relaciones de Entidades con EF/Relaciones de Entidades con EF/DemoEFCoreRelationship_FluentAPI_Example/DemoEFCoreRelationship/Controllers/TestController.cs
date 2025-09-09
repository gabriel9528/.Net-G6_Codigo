using DemoEFCoreRelationship.Models.ManyToMany.Exercise1;
using DemoEFCoreRelationship.Models.ManyToMany.Exercise2;
using DemoEFCoreRelationship.Models.OneToMany;
using DemoEFCoreRelationship.Models.OneToOne;
using DemoEFCoreRelationship.Repo;
using Microsoft.AspNetCore.Mvc;

namespace DemoEFCoreRelationship.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly RepositoryOneToOne _repository;
        private readonly RepositoryOneToMany _repositoryOneToMany;
        private readonly RepositoryManyToMany _repositoryManyToMany;
        public TestController(RepositoryOneToOne repository, RepositoryOneToMany repositoryOneToMany, RepositoryManyToMany repositoryManyToMany)
        {
            _repository = repository;
            _repositoryOneToMany = repositoryOneToMany;
            _repositoryManyToMany = repositoryManyToMany;
        }

        #region one-to-one
        //One to one
        [HttpPost("CartCompany")]
        public async Task<IActionResult> AddCarCompany(CarCompany carCompany)
        {
            await _repository.AddCarCompany(carCompany);
            return Ok("Company Saved");
        }

        [HttpPost("CartModel")]
        public async Task<IActionResult> AddCarModel(CarModel carModel)
        {
            await _repository.AddCarModel(carModel);
            return Ok("Model Saved");
        }

        [HttpGet("CartCompany")]
        public async Task<IActionResult> GetCarCompanies()
        {
            var companies = await _repository.GetCarCompanies();
            return Ok(companies);
        }

        [HttpGet("CarModel")]
        public async Task<IActionResult> GetCarModels() => 
            Ok(await _repository.GetCarModels());

        // One to One - Employee and EmployeeAddress
        [HttpPost("Employee")]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            await _repository.AddEmployee(employee);
            return Ok("Employee Saved");
        }

        [HttpPost("EmployeeAddress")]
        public async Task<IActionResult> AddEmployeeAddress(EmployeeAddress employeeAddress)
        {
            await _repository.AddEmployeeAddress(employeeAddress);
            return Ok("Employee Address Saved");
        }

        [HttpGet("Employees")]
        public async Task<IActionResult> GetEmployees() =>
            Ok(await _repository.GetEmployees());

        [HttpGet("EmployeeAddresses")]
        public async Task<IActionResult> GetEmployeeAddresses() =>
            Ok(await _repository.GetEmployeeAddresses());
        #endregion

        #region one-to-many
        //one to many

        [HttpPost("Patient")]
        public async Task<IActionResult> AddPatient(Patient patient)
        {
            await _repositoryOneToMany.AddPatient(patient);
            return Ok("Patient Saved");
        }

        [HttpPost("Doctor")]
        public async Task<IActionResult> AddDoctor(Doctor doctor)
        {
            await _repositoryOneToMany.AddDoctor(doctor);
            return Ok("Doctor Saved");
        }

        [HttpGet("Patients")]
        public async Task<IActionResult> GetPatients() => 
            Ok(await _repositoryOneToMany.GetPatients());

        [HttpGet("Doctors")]
        public async Task<IActionResult> GetDoctors() => 
            Ok(await _repositoryOneToMany.GetDoctors());

        // One to Many - Author and Book
        [HttpPost("Author")]
        public async Task<IActionResult> AddAuthor(Author author)
        {
            await _repositoryOneToMany.AddAuthor(author);
            return Ok("Author Saved");
        }

        [HttpPost("Book")]
        public async Task<IActionResult> AddBook(Book book)
        {
            await _repositoryOneToMany.AddBook(book);
            return Ok("Book Saved");
        }

        [HttpGet("Authors")]
        public async Task<IActionResult> GetAuthors() =>
            Ok(await _repositoryOneToMany.GetAuthors());

        [HttpGet("Books")]
        public async Task<IActionResult> GetBooks() =>
            Ok(await _repositoryOneToMany.GetBooks());
        #endregion

        #region many-to-many
        //Many to many

        [HttpPost("Student")]
        public async Task<IActionResult> AddStudent(Student student)
        {
            await _repositoryManyToMany.AddStudent(student);
            return Ok("Student Saved");
        }

        [HttpPost("Subject")]
        public async Task<IActionResult> AddSubject(Subject subject)
        {
            await _repositoryManyToMany.AddSubject(subject);
            return Ok("Subject Saved");
        }

        [HttpGet("Students")]
        public async Task<IActionResult> GetStudents() => 
            Ok(await _repositoryManyToMany.GetStudents());

        [HttpGet("Subjects")]
        public async Task<IActionResult> GetSubjects() => 
            Ok(await _repositoryManyToMany.GetSubjects());

        [HttpPost("StudentSubject")]
        public async Task<IActionResult> AddStudentSubject(Test test)
        {
            await _repositoryManyToMany.AddStudentSubject(test);
            return Ok("Student Subject Saved");
        }

        [HttpGet("StudentSubjects")]
        public async Task<IActionResult> GetStudentSubjects() => 
            Ok(await _repositoryManyToMany.GetStudentSubjects());



        // Add a new Person
        [HttpPost("Person")]
        public async Task<IActionResult> AddPerson(Person person)
        {
            await _repositoryManyToMany.AddPerson(person);
            return Ok("Person Saved");
        }

        // Add a new Business
        [HttpPost("Business")]
        public async Task<IActionResult> AddBusiness(Business business)
        {
            await _repositoryManyToMany.AddBusiness(business);
            return Ok("Business Saved");
        }

        // Get all Persons
        [HttpGet("Persons")]
        public async Task<IActionResult> GetPersons() =>
            Ok(await _repositoryManyToMany.GetPersons());

        // Get all Businesses
        [HttpGet("Businesses")]
        public async Task<IActionResult> GetBusinesses() =>
            Ok(await _repositoryManyToMany.GetBusinesses());

        // Add a PersonBusiness relationship
        [HttpPost("PersonBusiness")]
        public async Task<IActionResult> AddPersonBusiness(Test2 test2)
        {
            await _repositoryManyToMany.AddPersonBusiness(test2);
            return Ok("Person-Business relationship Saved");
        }

        // Get all PersonBusiness relationships
        [HttpGet("PersonBusinesses")]
        public async Task<IActionResult> GetPersonBusinesses() =>
            Ok(await _repositoryManyToMany.GetPersonBusinesses());

        #endregion

    }
}
