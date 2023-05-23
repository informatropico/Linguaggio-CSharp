namespace Generics
{
    public class Employee : EntityBase
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}";
        }
    }

    public class Manager : Employee
    {
        public override string ToString()
        {
            return base.ToString() + " (Manager)";
        }
    }
}