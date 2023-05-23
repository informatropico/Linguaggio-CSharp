namespace Generics
{
    public static class RepositoryExtensions
    {
        public static void AddBatchExtension<T>(this IWriteRepository<T> repo, IEnumerable<T> elements)
        {
            foreach (var item in elements)
            {
                repo.Add(item);
            }
            repo.Save();
        }
    }
}