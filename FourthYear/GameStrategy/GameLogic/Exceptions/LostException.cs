using System;

namespace Strategy.GameLogic.Exceptions
{
    public class LostException : Exception
    {
        public LostException(string massage)
            : base(massage)
        {
        }
    }
}
