using System;

namespace Strategy.GameLogic.Exceptions
{
    public class WinException : Exception
    {
        public WinException(string massage)
            : base(massage)
        {
        }
    }
}
