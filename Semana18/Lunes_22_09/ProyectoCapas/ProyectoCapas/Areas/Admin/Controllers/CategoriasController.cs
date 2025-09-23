using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using ProyectoCapas.AccesoDatos.Data.Repository.IRepository;
using ProyectoCapas.Models;

namespace ProyectoCapas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CategoriasController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet()]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var item = Json(new { data = _contenedorTrabajo.ICategoriaRepository.GetAll(x => x.IsActive) });
            return item;
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.ICategoriaRepository.Add(categoria);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        #endregion


        #region Edit

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }
            else
            {
                Categoria categoria = new Categoria();
                categoria = _contenedorTrabajo.ICategoriaRepository.GetById(id);
                if(categoria == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(categoria);
                }
            }
        }


        [HttpPost]
        public IActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.ICategoriaRepository.Update(categoria);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        #endregion


        #region Delete

        [HttpDelete()]
        public IActionResult Delete(int id)
        {
            var objFromDB = _contenedorTrabajo.ICategoriaRepository.GetFirstOrDefault(x => x.Id == id && x.IsActive);
            if (objFromDB == null)
            {
                return Json(new { success = false, message = "La categoria no existe" });
            }

            _contenedorTrabajo.ICategoriaRepository.Delete(id);
            _contenedorTrabajo.Save();

            return Json(new { success = true, message = "Categoria borrada exitosamente" });
        }

        #endregion
    }

}
