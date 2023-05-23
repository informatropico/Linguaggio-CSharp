// See https://aka.ms/new-console-template for more information
using Generics;

void EmployeeAdded(object? sender, Employee item)
{
    System.Console.WriteLine($"Item {item.Id} added");
}

var employeeListRepository = new ListRepository<Employee>();
var organizationListRepository = new ListRepository<Organization>();

var employeeSqlRepository = new SqlRepository<Employee>(new StorageDbContext());
employeeSqlRepository.ItemAdded += EmployeeAdded;

var organizationSqlRepository = new SqlRepository<Organization>(new StorageDbContext());

var employees = new[]
{
    new Employee() { Name = "Nome e1" },
    new Manager() { Name = "Nome m1" },
    new Employee() { Name = "Nome e2" }
};

var organizations = new[]
{
    new Organization() { Name = "Nome o1" },
    new Organization() { Name = "Nome o2" }
};

AddBatch(employeeSqlRepository, employees);
organizationSqlRepository.AddBatchExtension(organizations);

WriteAllData(employeeSqlRepository);
WriteAllData(organizationSqlRepository);

void WriteAllData(IReadRepository<IEntity> repo)
{
    var elements = repo.GetAll().OrderBy(e => e.Id);
    System.Console.WriteLine("\n");
    foreach (var item in elements)
    {
        System.Console.WriteLine(item.ToString());
    }
}

// Generic Methods
void AddBatch<T>(IWriteRepository<T> repo, IEnumerable<T> elements)
{
    foreach (var item in elements)
    {
        repo.Add(item);
    }
    repo.Save();
}





