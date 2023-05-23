namespace ErrorHandling
{
    public class Calculator
    {
        public int Calculate(int number1, int number2, string operation)
        {
            if(operation is null)
            {
                throw new ArgumentNullException(nameof(operation), "Value cannot be null.");
            }
            if(operation == "/"){
                try
                {
                    return Divide(number1, number2);
                }
                catch(DivideByZeroException e)
                {
                    //throw;

                    //throw new ArithmeticException("Error during calculation", e);

                    throw new CalculationException(e);
                }
            }
            else
            {
                //throw new ArgumentOutOfRangeException(nameof(operation), "Unknown operation.");
                throw new CalculationOperationNotSupportedException(operation);
            }
        }

        private int Divide(int number, int divisor) => number / divisor;
    }
}