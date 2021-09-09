using System.Collections.Generic;

namespace Strategy.GameLogic.Tools
{
    class PriorityQueue
    {
        private readonly List<(int, string)> data;

        public PriorityQueue()
        {
            data = new List<(int, string)>();
        }

        public void Enqueue((int, string) item)
        {
            data.Add(item);
            int i = data.Count - 1; 
            while (i > 0)
            {
                int p = (i - 1) / 2;
                if (data[i].CompareTo(data[p]) >= 0)
                    break;
                (int, string) tmp = data[i];
                data[i] = data[p];
                data[p] = tmp;
                i = p;
            }
        }

        public (int, string) Dequeue()
        {
            int l = data.Count - 1;
            (int, string) fItem = data[0];
            data[0] = data[l];
            data.RemoveAt(l);

            --l;
            int p = 0;
            while (true)
            {
                int c = p * 2 + 1;
                if (c > l)
                    break;
                int r = c + 1;
                if (r <= l && data[r].CompareTo(data[c]) < 0)
                    c = r;
                if (data[p].CompareTo(data[c]) <= 0)
                    break;
                (int, string) tmp = data[p];
                data[p] = data[c];
                data[c] = tmp;
                p = c;
            }
            return fItem;
        }

        public int Count()
        {
            return data.Count;
        }
    }
}
