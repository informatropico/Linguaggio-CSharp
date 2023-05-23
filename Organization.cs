namespace Generics
{
    public class Organization : EntityBase
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}";
        }
    }
}