using ProyectoCapas.Models;

namespace ProyectoCapas.AccesoDatos.Data.Repository.IRepository
{
    public interface IArticuloRepository : IRepository<Articulo>
    {
        void Update(Articulo articulo);
        void Delete(int id);
    }
}
