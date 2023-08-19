namespace BowlingLibrary.Exceptions
{
    public class AmountKnockedDownException : ArgumentOutOfRangeException
    {
        private readonly int actualValue;
        public AmountKnockedDownException(int actualValue)
        :base()
        {
            this.actualValue = actualValue;
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
                return $"It is impossible to have {actualValue} knocked down.";
            }
        }
    }
}