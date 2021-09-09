using System;
using System.Collections.Generic;
using System.Linq;

namespace Solver
{
    public class Questions
    {
        private readonly List<Tuple<int,string>> _questions;
        public Questions()
        {
            _questions = new List<Tuple<int, string>>
            {
                new Tuple<int, string>(0, ""),
            };
        }
        public Tuple<int, string> GetQuestion(int status)
        {
            return _questions.FirstOrDefault(x => x.Item1 == status);
        }
    }
}
