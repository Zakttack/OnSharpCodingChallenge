namespace BowlingLibrary.Models
{
    public class Shot
    {
        public Shot()
        {
            PinsKnockedDown = 0;
            Result = ' ';
        }
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
    }
}