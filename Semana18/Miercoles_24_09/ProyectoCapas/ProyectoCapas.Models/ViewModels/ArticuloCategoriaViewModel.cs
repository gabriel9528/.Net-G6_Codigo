using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoCapas.Models.ViewModels
{
    public class ArticuloCategoriaViewModel
    {
        public Articulo Articulo { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
