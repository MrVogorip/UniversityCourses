using System.Collections.Generic;
using System.Linq;

namespace DecisionMaking
{
    public class WaldCriteria : ICriteria
    {
        public GameMatrix GameMatrix { get; set; }
        public WaldCriteria(GameMatrix gameMatrix)
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
            double max1 = mins.Max(a => a.Item2);
            List<(GameVector, double)> result = mins.Where(a => a.Item2 == max1).ToList();
            List<GameVector> ans = new List<GameVector>();
            foreach (var item in result)
            {
                ans.Add(item.Item1);
            }
            return ans;
        }
    }
}
