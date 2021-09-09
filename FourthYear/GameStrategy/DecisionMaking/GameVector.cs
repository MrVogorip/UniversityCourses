using System.Collections.Generic;
using System.Linq;

namespace DecisionMaking
{
    public class GameVector
    {
        public string Name { get; set; }
        public List<double> Values { get; set; }
        public int Key { get; set; }
        public GameVector(string name, List<double> values, int key)
        {
            Name = name;
            Values = values;
            Key = key;
        }
        public double Max()
        {
            return Values.Max();
        }
        public double Min()
        {
            return Values.Min();
        }
        public override string ToString()
        {
            string result = string.Empty;
            result = Name + ": ";
            for (int i = 0; i < Values.Count; i++)
            {
                result += Values[i] + " ";
            }
            result += "\n";
            return result;
        }
        public int Equals(GameVector other)
        {
            var compare = 0;
            if (Key == other.Key)
            {
                return compare;
            }
            var great = true;
            for (int i = 0; i < Values.Count; i++)
            {
                great = great && Values[i] >= other.Values[i];
            }
            if (great)
            {
                compare = 1;
            }
            else
            {
                var less = true;
                for (int i = 0; i < Values.Count; i++)
                {
                    less = less && Values[i] <= other.Values[i];
                }
                if (less)
                {
                    compare = -1;
                }
            }
            return compare;
        }
    }
}
