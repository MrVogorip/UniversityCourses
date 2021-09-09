namespace ChatServer
{
    static class RSA
    {
        static public bool IsTheNumberSimple(long n)
        {
            if (n < 2)
                return false;
            if (n == 2)
                return true;
            for (long i = 2; i < n; i++)
                if (n % i == 0)
                    return false;
            return true;
        }

        static public long Calculate_d(long m)
        {
            long d = m - 1;
            for (long i = 2; i <= m; i++)
                if ((m % i == 0) && (d % i == 0))
                {
                    d--;
                    i = 1;
                }
            return d;
        }

        static public long Calculate_e(long d, long m)
        {
            long e = 10;
            while (true)
            {
                if ((e * d) % m == 1)
                    break;
                else
                    e++;
            }
            return e;
        }
    }
}
