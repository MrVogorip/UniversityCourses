namespace Network
{
    public class Vector
    {
        private double[] _v;
        public int N { get; set; }
        public Vector(int n)
        {
            N = n;
            _v = new double[n];
        }
        public Vector(params double[] values)
        {
            N = values.Length;
            _v = new double[N];
            for (int i = 0; i < N; i++)
                _v[i] = values[i];
        }
        public double this[int i]
        {
            get { return _v[i]; }
            set { _v[i] = value; }
        }
    }
}
