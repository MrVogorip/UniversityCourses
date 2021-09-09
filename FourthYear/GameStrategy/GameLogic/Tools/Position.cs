namespace Strategy.GameLogic.Tools
{
    public class Position
    {
        public int I { get; set; }

        public int J { get; set; }

        public static bool operator ==(Position p1, Position p2)
        {
            return p1.I == p2.I && p1.J == p2.J;
        }

        public static bool operator !=(Position p1, Position p2)
        {
            return p1.I != p2.I || p1.J != p2.J;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
