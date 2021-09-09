using System;

namespace Strategy.GameLogic.Exceptions
{
    class NotEnoughLevelException : Exception
    {
        public NotEnoughLevelException(string massage)
            : base(massage)
        {
        }
    }
}
