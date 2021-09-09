using System;

namespace Strategy.GameLogic.Exceptions
{
    class NotEnoughCoinsException : Exception
    {
        public NotEnoughCoinsException(string massage)
            : base(massage)
        {
        }
    }
}
