using System;
using System.Collections.Generic;
using System.Linq;

namespace Solver
{
    public class Results
    {
        private readonly List<Tuple<int, string>> _results;
        public Results()
        {
            _results = new List<Tuple<int, string>>
            {
                new Tuple<int, string>(3, ""),
            };
        }
        public string GetResult(int status)
        {
            return _results.FirstOrDefault(x => x.Item1 == status).Item2;
        }
    }
}
