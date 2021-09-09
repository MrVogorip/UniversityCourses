using System.Collections.Generic;
using System.Linq;

namespace DecisionMaking
{
    public class HurwitzCriteria : ICriteria
    {
        private double optimisticKoef;
        public GameMatrix GameMatrix { get; set; }
        public HurwitzCriteria(GameMatrix gameMatrix, double OptimisticKoef)
        {
            GameMatrix = gameMatrix;
            optimisticKoef = OptimisticKoef;
        }
        private List<(GameVector, HurwitzParam)> GetHurwitzParams(GameMatrix matrix)
        {
            List<(GameVector, HurwitzParam)> result = new List<(GameVector, HurwitzParam)>();
            foreach (var a in matrix.Alternatives)
            {
                result.Add((a, new HurwitzParam(a.Max(), a.Min(), optimisticKoef)));
            }
            return result;
        }
        public List<GameVector> Optimum()
        {
            var hpar = GetHurwitzParams(GameMatrix);
            var maxHurw = hpar.Max(a => a.Item2.value);

            var result = hpar.Where(a => a.Item2.value == maxHurw).ToList();
            List<GameVector> ans = new List<GameVector>();
            foreach (var item in result)
            {
                ans.Add(item.Item1);
            }
            return ans;
        }
        class HurwitzParam
        {
            public double xmax;
            public double xmin;
            public double optXmax;
            public double value;
            public HurwitzParam(double xmax, double xmin, double optXmax)
            {
                this.xmax = xmax;
                this.xmin = xmin;
                this.optXmax = optXmax;
                value = xmax * optXmax + xmin * (1 - optXmax);
            }
        }
    }
}
