using System;

namespace HomeWorkDataBase.Control
{
    class MyException : Exception
    {
        public int ErrorCode { get; }
        public MyException(int val)
        {
            ErrorCode = val;
        }
    }
}
