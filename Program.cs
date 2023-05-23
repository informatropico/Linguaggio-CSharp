namespace LinqQueries
{
    using System;
    using EFCoreGetStart;
    public class Program
    {
        public static void Main(string[] args)
        {
            //MovieExamples();
            //CarsExamples();
            //EntityFrameworkExamples();
            //EntityFrameworkGetStarted();

            var analisiVisure = new AnalisiCsv();
            analisiVisure.Statistics("visure.csv");
        }

        private static void EntityFrameworkExamples()
        {
            var cars = ProcessCarFile("fuel.csv");
            InsertData(cars);
            QueryData();
        }

        private static void QueryData()
        {
            var db = new CarDb();

            var tenMostEfficentCars = db.Cars.OrderByDescending(c => c.Combined).ThenBy(c => c.Name).Take(10);
            var tenMostEfficentBMWCars = db.Cars.Where(c => c.Manufacturer == "BMW").OrderByDescending(c => c.Combined).ThenBy(c => c.Name).Take(10);
            var twoMostEfficentCarsByManufacturer = db.Cars.GroupBy(c => c.Manufacturer)
                                                           .Select(g => new
                                                            {
                                                                Name = g.Key,
                                                                Cars = g.OrderByDescending(c => c.Combined).Take(2)
                                                            });

            foreach (var car in tenMostEfficentCars)
            {
                System.Console.WriteLine($"{car.Manufacturer} {car.Name}: {car.Combined}");
            }

            foreach (var group in twoMostEfficentCarsByManufacturer)
            {
                System.Console.WriteLine($"{group.Name}");
                foreach (var car in group.Cars)
                {
                    System.Console.WriteLine($"\t{car.Name}: {car.Combined}");
                }
            }
        }

        private static void InsertData(IEnumerable<CarModel> cars)
        {
            var db = new CarDb();
            if (!db.Cars.Any())
            {
                foreach (var car in cars)
                {
                    db.Cars.Add(car);
                    System.Console.WriteLine("Car added");
                }
                db.SaveChanges();
            }
        }

        private static void EntityFrameworkGetStarted()
        {
            var db = new BloggingContext();

            // Note: This sample requires the database to be created before running.
            Console.WriteLine($"Database path: {db.DbPath}.");

            // Create
            Console.WriteLine("Inserting a new blog");
            db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
            db.SaveChanges();

            // Read
            Console.WriteLine("Querying for a blog");
            var blog = db.Blogs
                .OrderBy(b => b.BlogId)
                .First();

            // Update
            Console.WriteLine("Updating the blog and adding a post");
            blog.Url = "https://devblogs.microsoft.com/dotnet";
            blog.Posts.Add(
                new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
            db.SaveChanges();

            // Delete
            Console.WriteLine("Delete the blog");
            db.Remove(blog);
            db.SaveChanges();
        }

        public static void CarsExamples()
        {
            //var cars = ProcessFile("fuel.csv");
            var cars = ProcessCarFileWithCustomProjection("fuel.csv");
            var manufacturers = ProcessManufacturerFileWithCustomProjection("manufacturers.csv");

            FilteringOrderingProjecting(cars);
            JoiningGrouping(cars, manufacturers);
            Aggregating(cars, manufacturers);


        }

        private static void Aggregating(IEnumerable<Car> cars, IEnumerable<Manufacturer> manufacturers)
        {
            var query1 = cars.GroupBy(c => c.Manufacturer)
                            .Select(g => new
                            {
                                Name = g.Key,
                                Cars = g.Count(),
                                Max = g.Max(c => c.Combined), //deve iterare ogni volta
                                Min = g.Min(c => c.Combined),
                                Avg = g.Average(c => c.Combined)
                            })
                            .OrderByDescending(g => g.Max);

            var query2 = cars.GroupBy(c => c.Manufacturer)
                            .Select(g =>
                            {
                                var results = g.Aggregate(
                                    new CarStatistics(),           // accumulatore (istanziato una volta per gruppo)
                                    (acc, c) => acc.Accumulate(c), // invocato per ogni c (car)
                                    acc => acc.Compute());         // invocato una volta per gruppo

                                return new
                                {
                                    Name = g.Key,
                                    Cars = results.Total,
                                    Max = results.Max,
                                    Min = results.Min,
                                    Avg = results.Avg
                                };
                            })
                            .OrderByDescending(g => g.Max);

            foreach (var result in query2)
            {
                System.Console.WriteLine($"{result.Name} ({result.Cars} cars)");
                System.Console.WriteLine($"\t Max: {result.Max}");
                System.Console.WriteLine($"\t Min: {result.Min}");
                System.Console.WriteLine($"\t Avg: {result.Avg}");
            }
        }

        private static void JoiningGrouping(IEnumerable<Car> cars, IEnumerable<Manufacturer> manufacturers)
        {
            var mostEfficentCarsOpt1 = cars.Join(manufacturers,
                                             car => car.Manufacturer,
                                             manufacturer => manufacturer.Name,
                                             (car, manufacturer) => new
                                             {
                                                 manufacturer.Headquarters,
                                                 car.Name,
                                                 car.Manufacturer,
                                                 car.Combined
                                             }
                                            )
                                       .OrderByDescending(car => car.Combined)
                                       .ThenBy(car => car.Name);

            var mostEfficentCarsOpt2 = cars.Join(manufacturers,
                                             car => car.Manufacturer,
                                             manufacturer => manufacturer.Name,
                                             (car, manufacturer) => new
                                             {
                                                 Car = car,
                                                 Manufacturer = manufacturer
                                             }
                                            )
                                       .OrderByDescending(car => car.Car.Combined)
                                       .ThenBy(car => car.Car.Name)
                                       .Select(car => new
                                       {
                                           car.Manufacturer.Headquarters,
                                           car.Car.Name,
                                           car.Car.Manufacturer,
                                           car.Car.Combined
                                       });

            var mostEfficentCarsForYear = cars.Join(manufacturers,
                                             car => new { car.Manufacturer, car.Year },
                                             manufacturer => new { Manufacturer = manufacturer.Name, manufacturer.Year },
                                             (car, manufacturer) => new
                                             {
                                                 manufacturer.Headquarters,
                                                 car.Name,
                                                 car.Manufacturer,
                                                 car.Combined
                                             }
                                            )
                                       .OrderByDescending(car => car.Combined)
                                       .ThenBy(car => car.Name);

            var mostEfficentCarPerManufacturer = cars.GroupBy(car => car.Manufacturer.ToUpper())
                                                                               .OrderBy(group => group.Key);

            var mostEfficentCarPerManufacturerWithCountry = manufacturers.GroupJoin(
                                                                                    cars,
                                                                                    manufacturer => manufacturer.Name,
                                                                                    car => car.Manufacturer,
                                                                                    (manufacturer, group) => new
                                                                                    {
                                                                                        Manufacturer = manufacturer,
                                                                                        Cars = group
                                                                                    }
                                                                                    )
                                                                          .OrderBy(manufacturer => manufacturer.Manufacturer.Name);

            var mostEfficentCarsPerCountry = mostEfficentCarPerManufacturerWithCountry.GroupBy(manufacturer => manufacturer.Manufacturer.Headquarters);


            foreach (var group in mostEfficentCarPerManufacturer)
            {
                System.Console.WriteLine($"{group.Key} has {group.Count()} cars");
                foreach (var car in group.OrderByDescending(car => car.Combined).Take(2))
                {
                    System.Console.WriteLine($"\t{car.Name}: {car.Combined}");
                }
            }

            System.Console.WriteLine("\n\n");

            foreach (var group in mostEfficentCarPerManufacturerWithCountry)
            {
                System.Console.WriteLine($"{group.Manufacturer.Name} ({group.Manufacturer.Headquarters}) has {group.Cars.Count()} cars");
                foreach (var car in group.Cars.OrderByDescending(car => car.Combined).Take(2))
                {
                    System.Console.WriteLine($"\t{car.Name}: {car.Combined}");
                }
            }

            System.Console.WriteLine("\n\n");

            foreach (var group in mostEfficentCarsPerCountry)
            {
                System.Console.WriteLine($"{group.Key}");
                foreach (var car in group.SelectMany(g => g.Cars).OrderByDescending(c => c.Combined).Take(3))
                {
                    System.Console.WriteLine($"\t{car.Name}: {car.Combined}");
                }
            }


        }

        private static void FilteringOrderingProjecting(IEnumerable<Car> cars)
        {
            var mostEfficentCars = cars.OrderByDescending(car => car.Combined)
                                       .ThenBy(car => car.Name);

            var mostEfficentCarsForManufacturerAndYear = cars.Where(car => car.Manufacturer == "BMW" && car.Year == 2016)
                                                            .OrderByDescending(car => car.Combined)
                                                            .ThenBy(car => car.Name);

            var topEfficientCarForManufacturerAndYear = mostEfficentCarsForManufacturerAndYear.First();
            var worsteEfficientCarForManufacturerAndYear = mostEfficentCarsForManufacturerAndYear.Last();

            var IsOverTopCars = cars.Any(car => car.Combined > 60);
            System.Console.WriteLine(IsOverTopCars);

            var AreAllOfManufacturer = cars.All(car => car.Manufacturer.ToLower() == "toyota");
            System.Console.WriteLine(AreAllOfManufacturer);

            var IsThereAtLeastOneOfManufaturer = cars.Any(car => car.Manufacturer.ToLower() == "toyota");
            System.Console.WriteLine(IsThereAtLeastOneOfManufaturer);

            System.Console.WriteLine("\n\n");
            foreach (var car in mostEfficentCars.Take(10))
            {
                System.Console.WriteLine($"{car.Manufacturer} {car.Name}: {car.Combined}");
            }

            System.Console.WriteLine("\n\n");

            System.Console.WriteLine($"{worsteEfficientCarForManufacturerAndYear.Manufacturer} {worsteEfficientCarForManufacturerAndYear.Name}: {worsteEfficientCarForManufacturerAndYear.Combined}");
            System.Console.WriteLine($"{topEfficientCarForManufacturerAndYear.Manufacturer} {topEfficientCarForManufacturerAndYear.Name}: {topEfficientCarForManufacturerAndYear.Combined}");

            System.Console.WriteLine("\n\n");

            var subsetOfCarsProperty = cars.Select(car => new { car.Manufacturer, car.Name });

            foreach (var car in subsetOfCarsProperty.Take(10))
            {
                System.Console.WriteLine($"{car.Manufacturer} {car.Name}");
            }

            var result = subsetOfCarsProperty.Take(2).SelectMany(car => car.Name);
            foreach (var character in result)
            {
                System.Console.WriteLine(character);
            }
        }

        private static IEnumerable<Car> ProcessFile(string path)
        {
            var cars = File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .Select(line => Car.ParseFromCsv(line));

            return cars.ToList();
        }

        private static IEnumerable<Car> ProcessCarFileWithCustomProjection(string path)
        {
            var cars = File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .ToCar();

            return cars.ToList();
        }

        private static IEnumerable<CarModel> ProcessCarFile(string path)
        {
            var cars = File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .ToCarModel();

            return cars.ToList();
        }

        private static IEnumerable<Manufacturer> ProcessManufacturerFileWithCustomProjection(string path)
        {
            var manufacturers = File.ReadAllLines(path)
                    .Where(line => line.Length > 1)
                    .ToManufacturer();

            return manufacturers.ToList();
        }

        public static void MovieExamples()
        {
            var movies = new List<Movie>
            {
                new Movie { Title = "The Dark Knight", Rating = 8.9f, Year = 2008 },
                new Movie { Title = "The King's Speech", Rating = 8.0f, Year = 2010 },
                new Movie { Title = "Casa Blanca", Rating = 8.5f, Year = 1942 },
                new Movie { Title = "Star Wars V", Rating = 8.7f, Year = 1980 }
            };

            var query = movies.Where(m => m.Year > 2000);
            foreach (var movie in query)
            {
                System.Console.WriteLine(movie.Title);
            }

            System.Console.WriteLine("\n\n");

            query = movies.Filter(m => m.Year > 2000).Take(1);
            foreach (var movie in query)
            {
                System.Console.WriteLine(movie.Title);
            }

            System.Console.WriteLine("\n\n");

            query = movies.YieldFilter(m => m.Year > 2000).Take(1);
            foreach (var movie in query)
            {
                System.Console.WriteLine(movie.Title);
            }

            System.Console.WriteLine("\n\n");

            query = movies.YieldFilter(m => m.Year > 2000);
            System.Console.WriteLine($"{query.Count()} movies");
            foreach (var movie in query)
            {
                System.Console.WriteLine(movie.Title);
            }

            System.Console.WriteLine("\n\n");

            query = movies.YieldFilter(m => m.Year > 2000).ToList();
            System.Console.WriteLine($"{query.Count()} movies");
            foreach (var movie in query)
            {
                System.Console.WriteLine(movie.Title);
            }
        }
    }
}