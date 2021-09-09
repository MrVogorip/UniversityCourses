using System;
using System.Collections.Generic;

namespace branch_and_bound.visualization.Algorithm
{
    public class Node
    {
        public List<Tuple<int, int>> Path;
        public int[,] ReducedMatrix;
        public int Cost { get; set; }
        public int Number { get; set; }
        public int Level { get; set; }
        public Node()
        {
            Path = new List<Tuple<int, int>>();
            ReducedMatrix = new int[GlobalVar.N, GlobalVar.N];
        }
        public Node(List<Tuple<int, int>> path, int[,] matrix)
        {
            Path = new List<Tuple<int, int>>();
            ReducedMatrix = new int[GlobalVar.N, GlobalVar.N];
            for (int n = 0; n < path.Count; n++)
                Path.Add(path[n]);
            for (int i = 0; i < ReducedMatrix.GetLength(0); i++)
                for (int j = 0; j < ReducedMatrix.GetLength(1); j++)
                    ReducedMatrix[i, j] = matrix[i, j];
        }
    }
}
