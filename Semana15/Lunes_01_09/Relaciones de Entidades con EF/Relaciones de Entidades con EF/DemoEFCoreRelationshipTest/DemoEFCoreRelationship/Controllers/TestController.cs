using DemoEFCoreRelationship.Models.ManyToMany;
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


        [HttpPost("AssignStudentToSubject")]
        public async Task<IActionResult> AssignStudentToSubject(int studentId, int subjectId)
        {
            await _repositoryManyToMany.AddStudentSubject(studentId, subjectId);
            return Ok("Student assigned to Subject");
        }

        [HttpGet("GetAllStudentSubjects")]
        public async Task<IActionResult> GetAllStudentSubjects()
        {
            var studentSubjects = await _repositoryManyToMany.GetAllStudentSubjects();
            if (studentSubjects == null || studentSubjects.Count == 0)
            {
                return NotFound("No assignments found.");
            }
            return Ok(studentSubjects);
        }

    }
}
