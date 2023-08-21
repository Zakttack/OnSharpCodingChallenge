namespace BowlingLibrary.Models
{
    public class Shot
    {
        public Shot(int pinsKnockedDown)
        {
            PinsKnockedDown = pinsKnockedDown;
            Result = ' ';
        }

        public int PinsKnockedDown
        {
            get;
            private set;
        }

        public char Result
        {
            get;
            set;
        }

        public static Shot Empty
        {
            get
            {
                return new(0);
            }
        }
    }
}