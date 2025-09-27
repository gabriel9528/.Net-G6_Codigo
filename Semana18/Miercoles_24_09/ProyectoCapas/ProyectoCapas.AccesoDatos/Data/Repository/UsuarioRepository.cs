using ProyectoCapas.AccesoDatos.Data.Repository.IRepository;
using ProyectoCapas.Models;

namespace ProyectoCapas.AccesoDatos.Data.Repository
{
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UsuarioRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void BloquearUsuario(string userId)
        {
            var usuario = _dbContext.ApplicationUsers.FirstOrDefault(x => x.Id == userId);
            usuario.LockoutEnd = DateTime.Now.AddYears(100);
            _dbContext.SaveChanges();
        }

        public void DesbloquearUsuario(string userId)
        {
            var usuario = _dbContext.ApplicationUsers.FirstOrDefault(x => x.Id == userId);
            usuario.LockoutEnd = DateTime.Now;
            _dbContext.SaveChanges();
        }
    }
}
