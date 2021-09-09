using System.Collections.Generic;
using System.Linq;

namespace Bayesian
{
    class EnumerationAlgorithm
    {
        private Node[] _nodes;
        private int _domain = 2;
        private int[] _currentEvidence;
        public EnumerationAlgorithm (Node[] n, int[] e)
        {
            _nodes = n;
            _currentEvidence = e;
        }
        private double[] Normalization(double[] q)
        {
            double sum = q.Sum();
            q[0] /= sum;
            q[1] /= sum;
            return q;
        }
        public double[] EnumerationAsk(int nodeId)
        {
            double[] Q = new double[2];
            var vars = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
            int[] initialEvidence = (int[])_currentEvidence.Clone();
            if (initialEvidence[nodeId] != -1)
            {
                Q[initialEvidence[nodeId]] = 1;
                return Normalization(Q);
            }
            for (int i = 0; i < _domain; ++i)
            {
                initialEvidence[nodeId] = i;
                Q[i] = EumerateAll(vars, initialEvidence);
            }
            return Normalization(Q);
        }
        private double EumerateAll(List<int> realVars, int[] realEvidence)
        {
            List<int> vars = new List<int>(realVars);
            int[] evidence = (int[])realEvidence.Clone();
            if (vars.Count == 0)
                return 1.0;
            int Y = vars[0];
            vars.RemoveAt(0);
            if (evidence[Y] != -1)
            {
                int j = 0, e1 = -1, e2 = -1;
                foreach (int parent in _nodes[Y].Parents)
                {
                    if (evidence[parent] != -1)
                    {
                        if (j == 0)
                        {
                            e1 = evidence[parent];
                            j = 1;
                        }
                        else
                        {
                            e2 = evidence[parent];
                        }
                    }
                }
                return _nodes[Y].GetTruthTableProbability(evidence[Y], e1, e2) * EumerateAll(vars, evidence);
            }
            else
            {
                double sum = 0.0;
                int[] new_evidence = (int[])evidence.Clone();
                for (int i = 0; i < _domain; ++i)
                {
                    new_evidence[Y] = i;
                    int j = 0, e1 = -1, e2 = -1;
                    foreach (int parent in _nodes[Y].Parents)
                    {
                        if (evidence[parent] != -1)
                        {
                            if (j == 0)
                            {
                                e1 = evidence[parent];
                                j = 1;
                            }
                            else
                            {
                                e2 = evidence[parent];
                            }
                        }
                    }
                    sum += _nodes[Y].GetTruthTableProbability(i, e1, e2) * EumerateAll(vars, new_evidence);
                }
                return sum;
            }
        }
    }
}
