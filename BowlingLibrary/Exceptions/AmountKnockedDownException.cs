namespace BowlingLibrary.Exceptions
{
    public class AmountKnockedDownException : ArgumentOutOfRangeException
    {
        private readonly int actualValue;
        private readonly int shotNumber;
        public AmountKnockedDownException(int actualValue, int shotNumber)
        :base()
        {
            this.actualValue = actualValue;
            this.shotNumber = shotNumber;
        }

        public override object ActualValue
        {
            get
            {
                return actualValue;
            }
        }

        public override string Message
        {
            get
            {
                string message = $"It is impossible to have {actualValue} knocked down {shotNumber + 1}";
                _ = shotNumber switch
                {
                    0 => message += $"st",
                    1 => message += $"nd",
                    2 => message += $"rd",
                    _ => message += ""
                };
                return message;
            }
        }
    }
}