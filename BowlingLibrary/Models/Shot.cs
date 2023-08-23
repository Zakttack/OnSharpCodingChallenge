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
            Shot other = (Shot)obj;
            return PinsKnockedDown == other.PinsKnockedDown && Result == other.Result;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator==(Shot a, Shot b)
        {
            try
            {
                return a.Equals(b);
            }
            catch (NullReferenceException)
            {
                try
                {
                    return b.Equals(a);
                }
                catch (NullReferenceException)
                {
                    return true;
                }
            }
        }

        public static bool operator!=(Shot a, Shot b)
        {
            try
            {
                return !a.Equals(b);
            }
            catch (NullReferenceException)
            {
                try
                {
                    return !b.Equals(a);
                }
                catch (NullReferenceException)
                {
                    return false;
                }
            }
        }
    }
}