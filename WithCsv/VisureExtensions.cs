namespace LinqQueries
{
    public static class VisureExtensions
    {
        public static IEnumerable<Visure> ToVisure(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(';');
                yield return new Visure
                {
                    PM = columns[0],
                    NumeroVisure2022 = int.Parse(columns[1])
                };

            }
        }
    }
}