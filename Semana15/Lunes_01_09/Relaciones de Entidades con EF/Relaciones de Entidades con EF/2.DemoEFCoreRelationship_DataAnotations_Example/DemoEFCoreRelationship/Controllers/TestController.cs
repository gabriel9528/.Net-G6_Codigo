using DemoEFCoreRelationship.Models.ManyToMany;
using DemoEFCoreRelationship.Models.OneToMany;
using DemoEFCoreRelationship.Models.OneToOne;
using DemoEFCoreRelationship.Repo.ManyToMany;
using DemoEFCoreRelationship.Repo.OneToMany;
using DemoEFCoreRelationship.Repo.OneToOne;
using Microsoft.AspNetCore.Mvc;

namespace DemoEFCoreRelationship.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly RepositoryCarCompanyModel _repository;
        private readonly RepositoryDoctorPattient _repositoryOneToMany;
        private readonly RepositoryStudentSubject _repositoryManyToMany;
        public TestController(RepositoryCarCompanyModel repository, RepositoryDoctorPattient repositoryOneToMany, RepositoryStudentSubject repositoryManyToMany)
        {
            _repository = repository;
            _repositoryOneToMany = repositoryOneToMany;
            _repositoryManyToMany = repositoryManyToMany;
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

        [HttpGet("Students")]
        public async Task<IActionResult> GetStudents() => 
            Ok(await _repositoryManyToMany.GetStudents());

        [HttpGet("Subjects")]
        public async Task<IActionResult> GetSubjects() => 
            Ok(await _repositoryManyToMany.GetSubjects());

    }
}
