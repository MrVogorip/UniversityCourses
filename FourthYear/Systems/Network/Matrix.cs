using System;

namespace Network
{
    public class Matrix
    {
        private double[][] _v;
        public int N { get; set; }
        public int M { get; set; }
        public Matrix(int n, int m, Random random)
        {
            N = n;
            M = m;
            _v = new double[n][];
            for (int i = 0; i < n; i++)
            {
                _v[i] = new double[m];

                for (int j = 0; j < m; j++)
                    _v[i][j] = random.NextDouble() - 0.5;
            }
        }
        public double this[int i, int j]
        {
            get { return _v[i][j]; }
            set { _v[i][j] = value; }
        }
    }
}
