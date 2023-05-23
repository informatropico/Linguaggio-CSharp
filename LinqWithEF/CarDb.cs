namespace LinqQueries
{
    using Microsoft.EntityFrameworkCore;
    using System;

    public class CarDb : DbContext
    {
        public DbSet<CarModel> Cars { get; set; }
        public string DbPath { get; }

        public CarDb()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "car.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}")
                      .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        
    }
}