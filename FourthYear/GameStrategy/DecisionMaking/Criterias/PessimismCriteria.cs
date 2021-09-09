using System.Collections.Generic;
using System.Linq;

namespace DecisionMaking
{
    class PessimismCriteria : ICriteria
    {
        public GameMatrix GameMatrix { get; set; }
        public PessimismCriteria(GameMatrix gameMatrix)
        {
            GameMatrix = gameMatrix;
        }
        public List<GameVector> Optimum()
        {
            List<(GameVector, double)> mins = new List<(GameVector, double)>();
            foreach (var item in GameMatrix.Alternatives)
            {
                mins.Add((item, item.Min()));
            }
            double min1 = mins.Min(a => a.Item2);
            List<(GameVector, double)> result = mins.Where(a => a.Item2 == min1).ToList();
            List<GameVector> ans = new List<GameVector>();
            foreach (var item in result)
            {
                ans.Add(item.Item1);
            }
            return ans;
        }
    }
}
