namespace ErrorHandling
{
    public class Program
    {
        public static void Main (string[] args)
        {
            var calc = new Calculator();

            System.Console.WriteLine("First number:");
            int number1 = Int32.Parse(Console.ReadLine());

            System.Console.WriteLine("Second number:");
            int number2 = Int32.Parse(Console.ReadLine());
            
            System.Console.WriteLine("Operation:");
            string operation = Console.ReadLine().ToUpperInvariant();


            try
            {
                var result = calc.Calculate(number1, number2, operation);
                System.Console.WriteLine(result);
            }
            catch(ArgumentException e) when (e.ParamName == "operation")
            {
                System.Console.WriteLine($"Filtered \n {e}");
            }
            catch(ArgumentNullException e)
            {
                System.Console.WriteLine(e);
            }
            catch(CalculationOperationNotSupportedException e)
            {
                System.Console.WriteLine(e);
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e);

            }

            Console.ReadLine();
        }
    }
}
