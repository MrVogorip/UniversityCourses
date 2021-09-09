using System.Collections.Generic;
using System.Linq;

namespace DecisionMaking
{
    public class GameMatrix
    {
        public List<List<double>> Matrix;
        public List<string> AlternativeNames;
        public List<string> NatureStateNames;
        public List<GameVector> Alternatives;
        public List<GameVector> NatureStates;
        public GameMatrix(List<List<double>> matrix, List<string> alternativeNames, List<string> natureStateNames)
        {
            Matrix = matrix;
            AlternativeNames = alternativeNames;
            NatureStateNames = natureStateNames;
            Alternatives = new List<GameVector>();
            for (int i = 0; i < Matrix.Count; i++)
            {
                string currAlternative = alternativeNames[i];
                GameVector gameVector = new GameVector(currAlternative, matrix[i], i);
                Alternatives.Add(gameVector);
            }
            NatureStates = new List<GameVector>();
            int lastIndex = matrix[0].Count;
            for (int j = 0; j < lastIndex; j++)
            {
                string currState = natureStateNames[j];
                List<double> states = new List<double>();
                for (int i = 0; i < matrix.Count; i++)
                {
                    states.Add(matrix[i][j]);
                }
                GameVector gameVector = new GameVector(currState, states, j);
                NatureStates.Add(gameVector);
            }
        }
        public GameMatrix Change(int i, int j, double value)
        {
            Matrix[i][j] = value;
            return new GameMatrix(Matrix, AlternativeNames, NatureStateNames);
        }
        public GameMatrix ChangeAlternativeName(int i, string value)
        {
            AlternativeNames[i] = value;
            return new GameMatrix(Matrix, AlternativeNames, NatureStateNames);
        }
        public GameMatrix ChangeNatureStateName(int j, string value)
        {
            NatureStateNames[j] = value;
            return new GameMatrix(Matrix, AlternativeNames, NatureStateNames);
        }
        public (int, int) Size()
        {
            return (Alternatives.Count, NatureStates.Count);
        }
        public override string ToString()
        {
            string result = string.Empty;
            result = "States of nature:\n";
            for (int i = 0; i < NatureStateNames.Count; i++)
            {
                result += NatureStateNames[i] + " ";
            }
            result += "\nGame Matrix:\n";
            for (int i = 0; i < Alternatives.Count; i++)
            {
                result += Alternatives[i].ToString() + " ";
            }
            result += "\n";
            return base.ToString();
        }
        public List<GameVector> DominateSet(List<GameVector> gvl, ref List<string> list, int dvv)
        {
            List<GameVector> dSet = new List<GameVector>();
            foreach (GameVector gv in gvl)
            {
                foreach (GameVector gvv in gvl)
                {
                    if (!dSet.Contains(gv) && !dSet.Contains(gvv))
                    {
                        if (gv.Equals(gvv) == dvv)
                        {
                            dSet.Add(gv);
                            list.Add($"{gvv} dominates {gv}");
                        }
                    }
                }
            }
            return dSet;
        }
        public GameMatrix NewMatrix(List<GameVector> dCol, List<GameVector> dRow)
        {
            List<List<double>> result = new List<List<double>>();
            List<string> ralternativeNames = new List<string>();
            List<string> rnatureStateNames = new List<string>();
            for (int i = 0; i < NatureStateNames.Count; i++)
            {
                if (!dCol.Contains(dCol[i]))
                {
                    rnatureStateNames.Add(NatureStateNames[i]);
                }
            }

            foreach (GameVector gv in Alternatives)
            {
                if (!dRow.Contains(gv))
                {
                    List<double> nr = new List<double>();
                    for (int i = 0; i < gv.Values.Count; i++)
                    {
                        if (!dCol.Contains(dCol[i]))
                        {
                            nr.Add(gv.Values[i]);
                        }
                    }
                    result.Add(nr);
                    ralternativeNames.Add(gv.Name);
                }
            }

            return new GameMatrix(result, ralternativeNames, rnatureStateNames);
        }
        public (GameMatrix, List<string>) DominateMatrix()
        {
            List<string> list = new List<string>();
            List<GameVector> dCol = DominateSet(NatureStates, ref list, 1);
            List<GameVector> dRow = DominateSet(NatureStates, ref list, -1);
            GameMatrix newMatrix = NewMatrix(dCol, dRow);
            (GameMatrix, List<string>) ddgm = (newMatrix, list);
            (GameMatrix, List<string>) ret = Iterate(ddgm, list);
            return ret;
        }
        public (GameMatrix, List<string>) Iterate((GameMatrix, List<string>) ddgm, List<string> list)
        {
            GameMatrix dgm = this;
            (GameMatrix, List<string>) lddgm = ddgm;
            while (dgm.Size() != lddgm.Item1.Size())
            {
                dgm = lddgm.Item1;
                list.AddRange(lddgm.Item2);
                lddgm = dgm.DominateMatrix();
            }
            return (dgm, list.Distinct().ToList());
        }
        public double MinClearPrice()
        {
            List<double> map = new List<double>();
            foreach (var item in Alternatives)
            {
                map.Add(item.Min());
            }
            return map.Max();
        }
        public double MaxClearPrice()
        {
            List<double> map = new List<double>();
            foreach (var item in Alternatives)
            {
                map.Add(item.Max());
            }
            return map.Min();
        }
        public bool ExistsClearStrategy()
        {
            return MinClearPrice() >= MaxClearPrice();
        }

    }
}
