using Microsoft.AspNetCore.Mvc;
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
            var item = Json(new { data = _contenedorTrabajo.ICategoriaRepository.GetAll() });
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

    }
}
