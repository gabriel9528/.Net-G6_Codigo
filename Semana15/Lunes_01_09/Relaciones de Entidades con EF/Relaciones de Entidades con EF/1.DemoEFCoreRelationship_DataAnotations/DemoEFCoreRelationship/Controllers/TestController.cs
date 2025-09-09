using DemoEFCoreRelationship.Models.ManyToMany;
using DemoEFCoreRelationship.Models.ManyToMany2;
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
        private readonly RepositoryStudenSubject _repositoryManyToMany;
        private readonly RepositoryPersonBusiness _repositoryPersonBusiness;
        public TestController(RepositoryOneToOne repository, 
            RepositoryOneToMany repositoryOneToMany, 
            RepositoryStudenSubject repositoryManyToMany,
            RepositoryPersonBusiness repositoryPersonBusiness)
        {
            _repository = repository;
            _repositoryOneToMany = repositoryOneToMany;
            _repositoryManyToMany = repositoryManyToMany;
            _repositoryPersonBusiness = repositoryPersonBusiness;
        }

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

        //Many to many2
        //Obtener todas las personas con sus empresas asociadas
        [HttpGet("GetPeopleWithBusinesses")]
        public async Task<ActionResult<List<Person>>> GetPeopleWithBusinesses()
        {
            var people = await _repositoryPersonBusiness.GetPeople();
            return Ok(people);
        }

        //Obtener todas las empresas con sus personas asociadas
        [HttpGet("GetBusinessesWithPeople")]
        public async Task<ActionResult<List<Business>>> GetBusinessesWithPeople()
        {
            var businesses = await _repositoryPersonBusiness.GetBusinesses();
            return Ok(businesses);
        }

        //Agregar una nueva persona
        [HttpPost("AddPerson")]
        public async Task<ActionResult> AddPerson(Person person)
        {
            await _repositoryPersonBusiness.AddPerson(person);
            return Ok();
        }

        //Agregar una nueva empresa
        [HttpPost("AddBusiness")]
        public async Task<ActionResult> AddBusiness(Business business)
        {
            await _repositoryPersonBusiness.AddBusiness(business);
            return Ok();
        }

    }
}
