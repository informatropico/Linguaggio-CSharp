namespace Generics
{
    /// <summary>
    ///     Covarianza
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadRepository<out T>
    {
        public T GetById(int id);
        public IEnumerable<T> GetAll();

    }

    /// <summary>
    ///     Controvarianza
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWriteRepository<in T>
    {
        public void Add(T element);
        
        public void Remove(T element);

        public void Save();
    }
    
    /// <summary>
    ///     Invarianza
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : IEntity
    {

    }
}