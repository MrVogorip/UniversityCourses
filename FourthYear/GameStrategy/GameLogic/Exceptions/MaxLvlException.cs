using System;

namespace Strategy.GameLogic.Exceptions
{
    class MaxLvlException : Exception
    {
        public MaxLvlException(string massage)
            : base(massage)
        {
        }
    }
}
