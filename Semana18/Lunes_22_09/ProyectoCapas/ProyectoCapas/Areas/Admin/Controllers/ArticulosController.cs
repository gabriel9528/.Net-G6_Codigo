using Microsoft.AspNetCore.Mvc;
using ProyectoCapas.AccesoDatos.Data.Repository.IRepository;
using ProyectoCapas.Models;
using ProyectoCapas.Models.ViewModels;

namespace ProyectoCapas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArticulosController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.IArticuloRepository.GetAll(x => x.IsActive, includeProperties: "Categoria") });
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            ArticuloCategoriaViewModel articuloCategoriaViewModel = new ArticuloCategoriaViewModel()
            {
                Articulo = new Articulo(),
                ListaCategorias = _contenedorTrabajo.ICategoriaRepository.GetListaCategorias()
            };

            //articuloCategoriaViewModel.ListaCategorias = _contenedorTrabajo.ICategoriaRepository.GetListaCategorias();
            return View(articuloCategoriaViewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloCategoriaViewModel model)
        {
            string mainRoute = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if(model.Articulo.Id == 0 && files.Count() > 0)
            {
                string nameFile = Guid.NewGuid().ToString();
                var upload = Path.Combine(mainRoute, @"imagenes\articulos");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(upload, nameFile + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStreams);
                }

                model.Articulo.UrlImagen = @"\imagenes\articulos\" + nameFile + extension;
                model.Articulo.FechaCreacion = DateTime.Now.ToString();

                _contenedorTrabajo.IArticuloRepository.Add(model.Articulo);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }

            model.ListaCategorias = _contenedorTrabajo.ICategoriaRepository.GetListaCategorias();
            return View(model);

        }


        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ArticuloCategoriaViewModel articuloCategoriaViewModel = new ArticuloCategoriaViewModel()
            {
                Articulo = _contenedorTrabajo.IArticuloRepository.GetById(id),
                ListaCategorias = _contenedorTrabajo.ICategoriaRepository.GetListaCategorias()
            };

            return View(articuloCategoriaViewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloCategoriaViewModel model)
        {
            string mainRoute = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var articulosDesdeDb = _contenedorTrabajo.IArticuloRepository.GetById(model.Articulo.Id);

            //En el caso que se suba una nueva imagen
            if (files.Count() > 0)
            {
                string nameFile = Guid.NewGuid().ToString();
                var upload = Path.Combine(mainRoute, @"imagenes\articulos");
                var extension = Path.GetExtension(files[0].FileName);

                //Comprobamos si existe la imagen
                var rutaImagenAntigua = Path.Combine(mainRoute, articulosDesdeDb.UrlImagen.TrimStart('\\'));
                if (System.IO.File.Exists(rutaImagenAntigua))
                {
                    System.IO.File.Delete(rutaImagenAntigua);
                }

                //NUEVAMENTE SUBIMOS EL ARCHIVO
                using (var fileStreams = new FileStream(Path.Combine(upload, nameFile + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStreams);
                }

                model.Articulo.UrlImagen = @"\imagenes\articulos\" + nameFile + extension;
                model.Articulo.FechaCreacion = DateTime.Now.ToString();

                _contenedorTrabajo.IArticuloRepository.Update(model.Articulo);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                model.Articulo.UrlImagen = articulosDesdeDb.UrlImagen;
            }

                model.ListaCategorias = _contenedorTrabajo.ICategoriaRepository.GetListaCategorias();
            return View(model);

        }


        #endregion

        #region Delete

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var oArticulo = _contenedorTrabajo.IArticuloRepository.GetById(id);
            string mainRoute = _webHostEnvironment.WebRootPath;
            var rootImage = Path.Combine(mainRoute, oArticulo.UrlImagen.TrimStart('\\'));

            //Comprobamos si existe la imagen
            if (System.IO.File.Exists(rootImage))
            {
                System.IO.File.Delete(rootImage);
            }

            if(oArticulo == null)
            {
                return Json(new { success = false, message = "Error, al eliminar el articulo" });
            }

            _contenedorTrabajo.IArticuloRepository.Delete(id);
            _contenedorTrabajo.Save();

            return Json(new { success = true, message = "Articulo eliminado exitosamente" });
        }

        #endregion
    }
}
