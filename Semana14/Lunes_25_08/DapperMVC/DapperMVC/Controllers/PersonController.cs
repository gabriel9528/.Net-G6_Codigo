using DapperMVC.Data.Models.Domain;
using DapperMVC.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DapperMVC.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<IActionResult> DisplayAll()
        {
            var listPersons = await _personRepository.GetAllPersonsAsync();
            return View(listPersons);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(person);
                }
                bool isSuccess = await _personRepository.AddPersonAsync(person);
                if (isSuccess)
                {
                    TempData["msg"] = "Persona agregada exitosamente";
                    return RedirectToAction("DisplayAll");
                }
                else
                {
                    TempData["msg"] = "Error al agregar la persona";
                    return View(person);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        #endregion

        #region Update


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Person person = null;
            try
            {
                person = await _personRepository.GetPersonByIdAsync(id);
                if (person == null)
                {
                    TempData["msg"] = "Error al obtener la persona";
                    return NotFound();
                }
                else
                {
                    return View(person);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Error " + ex.Message;
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(person);
                }
                bool isSuccess = await _personRepository.UpdatePersonAsync(person);
                if (isSuccess)
                {
                    TempData["msg"] = "Se edito la persona exitosamente";
                    return RedirectToAction("DisplayAll");
                }
                else
                {
                    TempData["msg"] = "Error al editar la persona";
                    return View(person);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Error " + ex.Message;
                return NotFound();
            }
        }

        #endregion

        #region delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool isSuccess = await _personRepository.DeletePersonAsync(id);
                if (isSuccess)
                {
                    TempData["msg"] = "Se elimino la persona exitosamente";
                }
                else
                {
                    TempData["msg"] = "Error al eliminar la persona";
                }
                return RedirectToAction("DisplayAll");
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Error " + ex.Message;
                return NotFound();
            }
        }


        #endregion
    }
}
