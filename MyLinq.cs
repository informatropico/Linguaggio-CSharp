namespace LinqQueries
{
    public static class MyLinq
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T>  source, Func<T, bool> predicate)
        {
            var result = new List<T>();

            foreach (var item in source)
            {
                if(predicate(item))
                {
                    result.Add(item);
                }
            }
            return result;

        }

        public static IEnumerable<T> YieldFilter<T>(this IEnumerable<T>  source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if(predicate(item))
                {
                    yield return item;
                }
            }

        }
    }
}