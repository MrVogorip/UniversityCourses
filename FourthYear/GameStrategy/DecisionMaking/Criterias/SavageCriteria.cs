using System.Collections.Generic;
using System.Linq;

namespace DecisionMaking
{
    public class SavageCriteria : ICriteria
    {
        public GameMatrix GameMatrix { get; set; }
        public SavageCriteria(GameMatrix gameMatrix)
        {
            GameMatrix = gameMatrix;
        }
        private List<(GameVector, double)> Risk(GameMatrix matrix)
        {
            List<(GameVector, double)> maxStates = new List<(GameVector, double)>();
            foreach (var item in matrix.NatureStates)
            {
                maxStates.Add((item, item.Values.Max()));
            }
            List<(GameVector, List<double>)> am = new List<(GameVector, List<double>)>();
            foreach (var a in matrix.Alternatives)
            {
                List<double> v = new List<double>();
                for (int i = 0; i < a.Values.Count; i++)
                {
                    var mn = maxStates[i].Item2;
                    v.Add(mn - a.Values[i]);
                }
                am.Add((a, v));
            }
            List<(GameVector, double)> result = new List<(GameVector, double)>();
            foreach (var m in am)
            {
                result.Add((m.Item1, m.Item2.Max()));
            }
            return result;
        }
        public List<GameVector> Optimum()
        {
            var risk = Risk(GameMatrix);
            double minRisk = risk.Min(a => a.Item2);

            List<(GameVector, double)> result = risk.Where(a => a.Item2 == minRisk).ToList();
            List<GameVector> ans = new List<GameVector>();
            foreach (var item in result)
            {
                ans.Add(item.Item1);
            }
            return ans;
        }
    }
}
