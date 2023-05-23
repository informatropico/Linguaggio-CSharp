using System.Text;

namespace LinqQueries
{
    public class AnalisiCsv
    {
        public void Statistics(string path)
        {
            var visure = ProcessFileWithCustomProjection(path);
            var statisticsFile = new StringBuilder();


            var PMPerNumeroVisureEffettuate = visure.GroupBy(v => v.NumeroVisure2022)
                                                    .Select(g => new
                                                    {
                                                        NumeroVisure = g.Key,
                                                        PM = g.Count()
                                                    })
                                                    .OrderBy(g => g.NumeroVisure);

            foreach (var g in PMPerNumeroVisureEffettuate)
            {
                var line = new string[] { g.NumeroVisure.ToString(), g.PM.ToString() };
                statisticsFile.AppendLine(string.Join(';', line));

                System.Console.WriteLine($"Numero Visure: {g.NumeroVisure}");
                System.Console.WriteLine($"Numero PM: {g.PM}");
                System.Console.WriteLine("\n");
            }

            File.AppendAllText("statistics.csv", statisticsFile.ToString());
        }
        private IEnumerable<Visure> ProcessFileWithCustomProjection(string path)
        {
            var visure = File.ReadAllLines(path)
                    .Where(line => line.Length > 1)
                    .ToVisure();

            return visure.ToList();
        }
    }
}