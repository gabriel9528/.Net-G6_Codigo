using Microsoft.EntityFrameworkCore;
using ProyectoCapas.AccesoDatos.Data.Repository.IRepository;
using System.Linq.Expressions;

namespace ProyectoCapas.AccesoDatos.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;

        internal DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }



        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
            string? includeProperties = null)
            //Resultado : ["Propiedad1", "Propiedad2"]
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if(orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();

        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T GetFirstOrDefault(
            Expression<Func<T, bool>>? filter = null, 
            string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault();
        }

    }
}
