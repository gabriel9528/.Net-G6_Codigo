using ProyectoCapas.AccesoDatos.Data.Repository.IRepository;
using ProyectoCapas.Data;
using ProyectoCapas.Models;

namespace ProyectoCapas.AccesoDatos.Data.Repository
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public SliderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var objFromDB = _dbContext.Sliders.FirstOrDefault(x => x.Id == id);
            if (objFromDB != null)
            {
                objFromDB.IsActive = false;
            }

            _dbContext.SaveChanges();
        }

        public void Update(Slider slider)
        {
            var objFromDB = _dbContext.Sliders.FirstOrDefault(x => x.Id == slider.Id);
            if (objFromDB != null)
            {
                objFromDB.Nombre = slider.Nombre;
                objFromDB.State = slider.State;
                objFromDB.UrlImagen = slider.UrlImagen;        
            }

            _dbContext.SaveChanges();
        }
    }
}
