using ProyectoCapas.AccesoDatos.Data.Repository.IRepository;
using ProyectoCapas.Models;

namespace ProyectoCapas.AccesoDatos.Data.Repository
{
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ArticuloRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var objFromDB = _dbContext.Articulos.FirstOrDefault(x => x.Id == id);
            if (objFromDB != null)
            {
                objFromDB.IsActive = false;
            }

            _dbContext.SaveChanges();
        }

        public void Update(Articulo articulo)
        {
            var objFromDB = _dbContext.Articulos.FirstOrDefault(x => x.Id == articulo.Id);
            if (objFromDB != null)
            {
                objFromDB.Nombre = articulo.Nombre;
                objFromDB.Descripcion = articulo.Descripcion;
                objFromDB.UrlImagen = articulo.UrlImagen;
                objFromDB.CategoriaId = articulo.CategoriaId;

            }

            _dbContext.SaveChanges();
        }
    }
}
