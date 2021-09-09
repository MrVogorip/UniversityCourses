using System;
using System.Collections.Generic;

namespace branch_and_bound.visualization.Algorithm
{
    class PriorityQueue
    {
        private List<Node> tree;
        private int size;
        public PriorityQueue()
        {
            tree = new List<Node>();
            tree.Add(null);
            size = 0;
        }
        public int getSize()
        {
            return size;
        }
        private void swap(int a, int b)
        {
            Node temp = tree[a];
            tree[a] = tree[b];
            tree[b] = temp;
        }
        public void add(Node newMatrix)
        {
            tree.Add(newMatrix);
            size++;
            int k = size;
            while (k > 1)
            {
                if (tree[k / 2].Cost > tree[k].Cost)
                    swap(k, k / 2);
                else
                    break;
                k = k / 2;
            }
        }
        public Node remove()
        {
            if (size == 0)
                return null;
            Node root = tree[1];
            tree[1] = tree[size];
            tree.RemoveAt(size);
            size--;
            int k = 1;
            Boolean flag = true;
            if (size == 0)
                return root;
            while (flag)
            {
                if (k * 2 > size)
                    break;
                flag = false;
                if (k * 2 == size)
                {
                    if (tree[k * 2].Cost > tree[k].Cost)
                    {
                        swap(k, k * 2);
                        k *= 2;
                        flag = true;
                    }
                    break;
                }
                if (tree[k * 2].Cost < tree[k * 2 + 1].Cost)
                {
                    if (tree[k * 2].Cost < tree[k].Cost)
                    {
                        swap(k, k * 2);
                        k *= 2;
                        flag = true;
                    }
                    else if (tree[k * 2 + 1].Cost < tree[k].Cost)
                    {
                        swap(k * 2 + 1, k);
                        k = k * 2 + 1;
                        flag = true;
                    }
                }
                else
                {
                    if (tree[k * 2 + 1].Cost < tree[k].Cost)
                    {
                        swap(k, k * 2 + 1);
                        k = k * 2 + 1;
                        flag = true;
                    }
                    else if (tree[k * 2].Cost < tree[k].Cost)
                    {
                        swap(k, k * 2);
                        k = k * 2;
                        flag = true;
                    }
                }
            }
            return root;
        }
    }
}
