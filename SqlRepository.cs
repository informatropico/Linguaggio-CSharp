using Microsoft.EntityFrameworkCore;

namespace Generics
{
    public class SqlRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<T> _dbSet;

        public SqlRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public event EventHandler<T>? ItemAdded;

        public void Add(T element)
        {
            _dbSet.Add(element);
            ItemAdded?.Invoke(this, element);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Remove(T element)
        {
            _dbSet.Remove(element);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}