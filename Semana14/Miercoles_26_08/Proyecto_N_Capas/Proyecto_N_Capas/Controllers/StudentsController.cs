using BLL.LogicServices;
using BOL.CommonEntities;
using BOL.DataBaseEntities;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_N_Capas.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudenService _studenService;
        public StudentsController(IStudenService studenService)
        {
            _studenService = studenService;
        }


        [HttpGet]
        public IActionResult GetStudents()
        {
            StudentModel studentModel = new StudentModel();
            studentModel.StudentList = _studenService.GetListStudentService();
            return View(studentModel);
        }

        #region POST
        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                bool result = _studenService.SaveStudentService(student);
                if (result)
                {
                    TempData["Message"] = "Estudiante guardado exitosamente.";
                    return RedirectToAction("GetStudents");
                }
                else
                {
                    TempData["Message"] = "Error al guardar el estudiante. Por favor, intente de nuevo.";
                    return View(student);
                }
            }
            return View(student);
        }
        #endregion

        #region EDIT
        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            var student = _studenService.GetListStudentService().FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult EditStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                bool result = _studenService.UpdateStudentService(student);
                if (result)
                {
                    TempData["Message"] = "Estudiante actualizado exitosamente.";
                    return RedirectToAction("GetStudents");
                }
                else
                {
                    TempData["Message"] = "Error al guardar el estudiante. Por favor, intente de nuevo.";
                    return View(student);
                }
            }
            return View(student);
        }
        #endregion

        #region DELETE
        [HttpGet]
        public IActionResult DeleteStudent(int id)
        {
            var student = _studenService.GetListStudentService().FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult DeleteStudent(Student student)
        {
            bool result = _studenService.DeleteStudentService(student.Id);
            if (result)
            {
                TempData["Message"] = "Estudiante eliminado exitosamente.";
                return RedirectToAction("GetStudents");
            }
            else
            {
                TempData["Message"] = "Error al eliminar el estudiante. Por favor, intente de nuevo.";
                return View();
            }
        }
        #endregion
    }
}
