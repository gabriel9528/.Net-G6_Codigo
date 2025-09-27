using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoCapas.Models;

namespace ProyectoCapas.AccesoDatos.Data.Repository.IRepository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        void Update(Categoria categoria);
        void Delete(int id);
        IEnumerable<SelectListItem> GetListaCategorias();
    }
}
