using System;
using System.Collections.Generic;

namespace Solver
{
    public class Table
    {
        private readonly List<(int, Tuple<int, string>)> _hashtable;
        public Table()
        {
            _hashtable = new List<(int, Tuple<int, string>)>
            {
                {( 1, new Tuple<int, string>(0, "") )},
            };
        }
        public List<(int, Tuple<int, string>)> GetAnswers(int status)
        {
            List<(int, Tuple<int, string>)> Answers = new List<(int, Tuple<int, string>)>();
            for (int i = 0; i < _hashtable.Count; i++)
            {
                if(_hashtable[i].Item2.Item1 == status)
                {
                    Answers.Add(_hashtable[i]);
                }
            }
            return Answers;
        }
    }
}
