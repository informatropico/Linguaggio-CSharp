namespace Generics
{
    public class ListRepository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly List<T> elements = new List<T>();
        
        
        public void Add (T element)
        {
            element.Id = elements.Count + 1;
            elements.Add(element);
        }

        public void Remove(T element)
        {
            this.elements.Remove(element);
        }

        public T GetById(int id)
        {
            return elements.First(e => e.Id == id);
        }

        public void Save()
        {
            foreach (var element in elements)
            {
                System.Console.WriteLine(element.ToString());
            }
        }

        public IEnumerable<T> GetAll()
        {
            return elements;
        }
    }
}