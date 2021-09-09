using System;
using System.Collections.Generic;

namespace branch_and_bound.visualization.Algorithm
{
    public static class AlgorithmBranchAndBound
    {
        private static void rowReduction(int[,] Matrix, int[] row)
        {
            for (int i = 0; i < row.Length; i++)
                row[i] = GlobalVar.INF;
            for (int i = 0; i < GlobalVar.N; i++)
                for (int j = 0; j < GlobalVar.N; j++)
                    if (Matrix[i, j] < row[i])
                        row[i] = Matrix[i, j];
            for (int i = 0; i < GlobalVar.N; i++)
                for (int j = 0; j < GlobalVar.N; j++)
                    if (Matrix[i, j] != GlobalVar.INF)
                        Matrix[i, j] -= row[i];
        }
        private static void colReduction(int[,] Matrix, int[] col)
        {
            for (int i = 0; i < col.Length; i++)
                col[i] = GlobalVar.INF;
            for (int i = 0; i < GlobalVar.N; i++)
                for (int j = 0; j < GlobalVar.N; j++)
                    if (Matrix[i, j] < col[j])
                        col[j] = Matrix[i, j];
            for (int i = 0; i < GlobalVar.N; i++)
                for (int j = 0; j < GlobalVar.N; j++)
                    if (Matrix[i, j] != GlobalVar.INF)
                        Matrix[i, j] -= col[j];
        }
        private static int CalculateCost(int[,] reducedMatrix)
        {
            int cost = 0;
            int[] row = new int[GlobalVar.N];
            rowReduction(reducedMatrix, row);
            int[] col = new int[GlobalVar.N];
            colReduction(reducedMatrix, col);
            for (int i = 0; i < GlobalVar.N; i++)
            {
                cost += (row[i] != GlobalVar.INF) ? row[i] : 0;
                cost += (col[i] != GlobalVar.INF) ? col[i] : 0;
            }
            return cost;
        }
        private static Node NewNode(int[,] parentMatrix, List<Tuple<int, int>> path, int lvl, int i, int j)
        {
            Node node = new Node(path, parentMatrix);
            if (lvl != 0) node.Path.Add(new Tuple<int, int>(i, j));
            for (int k = 0; lvl != 0 && k < GlobalVar.N; k++)
            {
                node.ReducedMatrix[i, k] = GlobalVar.INF;
                node.ReducedMatrix[k, j] = GlobalVar.INF;
            }
            for (int k = 0; k < node.Path.Count; k++)
                node.ReducedMatrix[node.Path[k].Item2, node.Path[k].Item1] = GlobalVar.INF;
            node.Level = lvl;
            node.Number = j;
            return node;
        }
        public static List<Tuple<int, int>> FindWay(int[,] costMatrix)
        {
            if (GlobalVar.N <= 2) throw new Exception("Path matrix dimension error.");
            PriorityQueue pq = new PriorityQueue();
            List<Tuple<int, int>> path = new List<Tuple<int, int>>();
            Node root = NewNode(costMatrix, path, 0, -1, 0);
            root.Cost = CalculateCost(root.ReducedMatrix);
            pq.add(root);
            while (pq.getSize() != 0)
            {
                Node min = pq.remove();
                int i = min.Number;
                if (min.Level == GlobalVar.N - 1)
                {
                    min.Path.Add(new Tuple<int, int>(i, 0));
                    return min.Path;
                }
                for (int j = 0; j < GlobalVar.N; j++)
                {
                    if (min.ReducedMatrix[i, j] != GlobalVar.INF)
                    {
                        Node child = NewNode(min.ReducedMatrix, min.Path, min.Level + 1, i, j);
                        child.Cost = min.Cost + min.ReducedMatrix[i, j] + CalculateCost(child.ReducedMatrix);
                        pq.add(child);
                    }
                }
            }
            return null;
        }
    }
}
