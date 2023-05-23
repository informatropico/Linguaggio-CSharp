namespace ErrorHandling
{
    public class CalculationException : Exception
    {
        private const string DefaultMessage = "Error in calculation.";

        public CalculationException(): base(DefaultMessage)
        {        
        }

        public CalculationException(string message) : base(message)
        {
        }

        public CalculationException(Exception innerException): base(DefaultMessage, innerException)
        {
        }

        public CalculationException(string message, Exception innerException) : base (message, innerException)
        {
        }
    }
}