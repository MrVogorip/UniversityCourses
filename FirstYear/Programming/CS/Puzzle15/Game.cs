using System;

namespace Puzzle15
{
    class Game : Interface
    {
        public int count { get; set; }
        int size;
        int[,] map;
        int sp_x, sp_y;
         static Random ra = new Random();
        public Game(int size)
        {
            if (size < 2) size = 2;
            if (size > 5) size = 5;
            this.size = size;
            map = new int[size, size];
        }
        public void start()
        {
            for (int x = 0; x < size; ++x)
                for (int y = 0; y < size; ++y)
                    map[x, y] = cor(x, y) + 1;
            sp_x = size - 1;
            sp_y = size - 1;
            map[sp_x, sp_y] = 0;
        }
        public int get_num(int pos)
        {
            int x, y;
            posit(pos, out x, out y);
            if (x < 0 || x >= size) return 0;
            if (y < 0 || y >= size) return 0;
            return map[x, y];
        }
        private int cor(int x,int y)
        {
            if (x < 0) x = 0;
            if (x > size - 1) x = size - 1;
            if (y < 0) y = 0;
            if (y > size - 1) y = size - 1;
            return y * size + x;
        }
        private void posit(int pos,out int x,out int y)
        {
            if (pos < 0) pos = 0;
            if (pos > size * size - 1) pos = size * size - 1;
            x = pos % size;
            y = pos / size;
        }
        public void shift(int pos)
        {
            int x, y;
            posit(pos, out x, out y);
            if (Math.Abs(sp_x - x) + Math.Abs(sp_y - y) != 1)
            {
                return;
            }
            map[sp_x, sp_y] = map[x, y];
            map[x, y] = 0;
            sp_x = x;
            sp_y = y;
            
            count++;
        }
        public void ran()
        {
            int a = ra.Next(0, 4);
            int x = sp_x;
            int y = sp_y;
            switch (a)
            {
                case 0: x--; break;
                case 1: x++; break;
                case 2: y--; break;
                case 3: y++; break;
            }
            shift(cor(x, y));
        }
        public bool check()
        {
            if (!(sp_x == size - 1 && sp_y == size - 1)) return false;
            for (int x = 0; x < size; ++x)
                for (int y = 0; y < size; ++y)
                    if (!(x == size - 1 && y == size - 1))
                        if (map[x, y] != cor(x, y) + 1)
                            return false;
            return true;

        }
    }
}
