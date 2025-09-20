using Microsoft.EntityFrameworkCore;
using ProyectoCapas.AccesoDatos.Data.Repository.IRepository;
using ProyectoCapas.Data;
using ProyectoCapas.Models;

namespace ProyectoCapas.AccesoDatos.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoriaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var objFromDB = _dbContext.Categorias.FirstOrDefault(x => x.Id == id);
            if (objFromDB != null)
            {
                objFromDB.IsActive = false;
            }

            _dbContext.SaveChanges();
        }

        public void Update(Categoria categoria)
        {
            var objFromDB = _dbContext.Categorias.FirstOrDefault(x => x.Id == categoria.Id);
            if (objFromDB != null)
            {
                objFromDB.Nombre = categoria.Nombre;
                objFromDB.Orden = categoria.Orden;
            }

            _dbContext.SaveChanges();
        }
    }
}
