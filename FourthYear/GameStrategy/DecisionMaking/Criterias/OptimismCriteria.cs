using System.Collections.Generic;
using System.Linq;

namespace DecisionMaking
{
    class OptimismCriteria : ICriteria
    {
        public GameMatrix GameMatrix { get; set; }
        public OptimismCriteria(GameMatrix gameMatrix)
        {
            GameMatrix = gameMatrix;
        }
        public List<GameVector> Optimum()
        {
            List<(GameVector, double)> maxs = new List<(GameVector, double)>();
            foreach (var item in GameMatrix.Alternatives)
            {
                maxs.Add((item, item.Max()));
            }
            double max1 = maxs.Max(a => a.Item2);
            List<(GameVector, double)> result = maxs.Where(a => a.Item2 == max1).ToList();
            List<GameVector> ans = new List<GameVector>();
            foreach (var item in result)
            {
                ans.Add(item.Item1);
            }
            return ans;
        }
    }
}
