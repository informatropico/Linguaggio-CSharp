namespace ErrorHandling
{
    public class CalculationOperationNotSupportedException : CalculationException
    {
        private const string DefaultMessage = "Operation not supported.";

        public string Operation { get; }

        public CalculationOperationNotSupportedException() : base(DefaultMessage)
        {
        }

        public CalculationOperationNotSupportedException(Exception innerException) : base(DefaultMessage, innerException)
        {
        }

        public CalculationOperationNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CalculationOperationNotSupportedException(string operation) : base(DefaultMessage) => Operation = operation;

        public CalculationOperationNotSupportedException(string operation, string message) : base(message) => Operation = operation;

        public override string ToString()
        {
            if (Operation is null)
            { 
                return base.ToString(); 
            }

            return base.ToString() + Environment.NewLine + $"Unsuppported operation: {Operation}";
        }
    }
}