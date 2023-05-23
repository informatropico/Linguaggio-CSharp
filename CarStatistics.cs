namespace LinqQueries
{
    public class CarStatistics
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public double Avg { get; set; }

        public CarStatistics()
        {
            Max = Int32.MinValue;
            Min = Int32.MaxValue;
        }
        public CarStatistics Accumulate(Car c)
        {
            Count += 1;
            Total += c.Combined;
            Max = Math.Max(Max, c.Combined);
            Min = Math.Min(Min, c.Combined);

            return this;
        }

        public CarStatistics Compute()
        {
            Avg = Total / Count;
            
            return this;

        }
    }
}