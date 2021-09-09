using System;

namespace Network
{
    public class NetworkAlgorithm
    {
        struct LayerT
        {
            public Vector x;
            public Vector z; 
            public Vector df;
        }
        private Matrix[] _weights; 
        private LayerT[] _l; 
        private Vector[] _deltas; 
        private int _layersN;
        public NetworkAlgorithm(int[] sizes)
        {
            Random random = new Random(DateTime.Now.Millisecond); 
            _layersN = sizes.Length - 1;
            _weights = new Matrix[_layersN]; 
            _l = new LayerT[_layersN]; 
            _deltas = new Vector[_layersN]; 
            for (int k = 1; k < sizes.Length; k++)
            {
                _weights[k - 1] = new Matrix(sizes[k], sizes[k - 1], random); 
                _l[k - 1].x = new Vector(sizes[k - 1]); 
                _l[k - 1].z = new Vector(sizes[k]); 
                _l[k - 1].df = new Vector(sizes[k]);
                _deltas[k - 1] = new Vector(sizes[k]); 
            }
        }
        public Vector Forward(Vector input)
        {
            for (int k = 0; k < _layersN; k++)
            {
                if (k == 0)
                {
                    for (int i = 0; i < input.N; i++)
                        _l[k].x[i] = input[i];
                }
                else
                {
                    for (int i = 0; i < _l[k - 1].z.N; i++)
                        _l[k].x[i] = _l[k - 1].z[i];
                }
                for (int i = 0; i < _weights[k].N; i++)
                {
                    double y = 0;
                    for (int j = 0; j < _weights[k].M; j++)
                        y += _weights[k][i, j] * _l[k].x[j];
                    _l[k].z[i] = 1 / (1 + Math.Exp(-y));
                    _l[k].df[i] = _l[k].z[i] * (1 - _l[k].z[i]);
                }
            }
            return _l[_layersN - 1].z;
        }

        private void UpdateWeights(double alpha)
        {
            for (int k = 0; k < _layersN; k++)
            {
                for (int i = 0; i < _weights[k].N; i++)
                {
                    for (int j = 0; j < _weights[k].M; j++)
                    {
                        _weights[k][i, j] -= alpha * _deltas[k][i] * _l[k].x[j];
                    }
                }
            }
        }
        private void Backward(Vector output, ref double error)
        {
            int last = _layersN - 1;
            error = 0;
            for (int i = 0; i < output.N; i++)
            {
                double e = _l[last].z[i] - output[i]; 
                _deltas[last][i] = e * _l[last].df[i]; 
                error += e * e / 2;
            }
            for (int k = last; k > 0; k--)
            {
                for (int i = 0; i < _weights[k].M; i++)
                {
                    _deltas[k - 1][i] = 0;
                    for (int j = 0; j < _weights[k].N; j++)
                        _deltas[k - 1][i] += _weights[k][j, i] * _deltas[k][j];
                    _deltas[k - 1][i] *= _l[k - 1].df[i];
                }
            }
        }
        public void Train(Vector[] X, Vector[] Y, double alpha, double eps, int epochs)
        {
            int epoch = 1; 
            double error; 
            do
            {
                error = 0; 
                for (int i = 0; i < X.Length; i++)
                {
                    Forward(X[i]);
                    Backward(Y[i], ref error);
                    UpdateWeights(alpha); 
                }
                epoch++;
            } while (epoch <= epochs && error > eps);
        }
    }
}
