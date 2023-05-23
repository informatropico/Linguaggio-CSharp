namespace LinqQueries
{
    public class Movie
    {
        public string Title { get; set; }
        public float Rating { get; set; }
        
        private int _year;
        public int Year 
        { 
            get
            {
                System.Console.WriteLine($"Return {_year} for {Title}");
                return _year;
            } 
            set
            {
                _year = value;
            } 
        }
    }
}