using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoCapas.AccesoDatos.Data.Repository.IRepository;
using System.Security.Claims;

namespace ProyectoCapas.Areas.Admin.Controllers
{

    [Authorize(Roles = "Administrador")]
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public UsuariosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //Obtenemos todos los usuarios menos el que esta en sesion para evitar que este se pueda bloquear asi mismo
            return View(_contenedorTrabajo.IUsuarioRepository.GetAll(x => x.Id != usuarioActual.Value));
        }

        [HttpGet]
        public IActionResult Bloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _contenedorTrabajo.IUsuarioRepository.BloquearUsuario(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Desbloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _contenedorTrabajo.IUsuarioRepository.DesbloquearUsuario(id);
            return RedirectToAction("Index");
        }
    }
}
