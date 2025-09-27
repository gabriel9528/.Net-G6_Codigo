using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoCapas.AccesoDatos.Data.Repository.IRepository;
using ProyectoCapas.Models;

namespace ProyectoCapas.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SlidersController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.ISliderRepository.GetAll(x => x.IsActive) });
        }

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider model)
        {
            string mainRoute = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (files.Count() > 0)
            {
                string nameFile = Guid.NewGuid().ToString();
                var upload = Path.Combine(mainRoute, @"imagenes\sliders");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(upload, nameFile + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStreams);
                }

                model.UrlImagen = @"\imagenes\sliders\" + nameFile + extension;

                _contenedorTrabajo.ISliderRepository.Add(model);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(model);

        }


        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var slider = _contenedorTrabajo.ISliderRepository.GetById(id);

            return View(slider);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider model)
        {
            string mainRoute = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var oSlider = _contenedorTrabajo.ISliderRepository.GetById(model.Id);

            //En el caso que se suba una nueva imagen
            if (files.Count() > 0)
            {
                string nameFile = Guid.NewGuid().ToString();
                var upload = Path.Combine(mainRoute, @"imagenes\sliders");
                var extension = Path.GetExtension(files[0].FileName);

                //Comprobamos si existe la imagen
                var rutaImagenAntigua = Path.Combine(mainRoute, oSlider.UrlImagen.TrimStart('\\'));
                if (System.IO.File.Exists(rutaImagenAntigua))
                {
                    System.IO.File.Delete(rutaImagenAntigua);
                }

                //NUEVAMENTE SUBIMOS EL ARCHIVO
                using (var fileStreams = new FileStream(Path.Combine(upload, nameFile + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStreams);
                }

                model.UrlImagen = @"\imagenes\sliders\" + nameFile + extension;

                _contenedorTrabajo.ISliderRepository.Update(model);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                model.UrlImagen = oSlider.UrlImagen;
            }

            _contenedorTrabajo.ISliderRepository.Update(model);
            _contenedorTrabajo.Save();

            return RedirectToAction(nameof(Index));

        }


        #endregion

        #region Delete

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var oSlider = _contenedorTrabajo.ISliderRepository.GetById(id);
            string mainRoute = _webHostEnvironment.WebRootPath;
            var rootImage = Path.Combine(mainRoute, oSlider.UrlImagen.TrimStart('\\'));

            //Comprobamos si existe la imagen
            if (System.IO.File.Exists(rootImage))
            {
                System.IO.File.Delete(rootImage);
            }

            if (oSlider == null)
            {
                return Json(new { success = false, message = "Error, al eliminar el slider" });
            }

            _contenedorTrabajo.ISliderRepository.Delete(id);
            _contenedorTrabajo.Save();

            return Json(new { success = true, message = "Slider eliminado exitosamente" });
        }

        #endregion
    }
}
