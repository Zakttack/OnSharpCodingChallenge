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

        public override bool Equals(object obj)
        {
            if (obj is not Shot)
            {
                return false;
            }
            return this == (Shot)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator==(Shot a, Shot b)
        {
            return a.PinsKnockedDown == b.PinsKnockedDown && a.Result == b.Result;
        }

        public static bool operator!=(Shot a, Shot b)
        {
            return a.PinsKnockedDown != b.PinsKnockedDown || a.Result != b.Result;
        }
    }
}