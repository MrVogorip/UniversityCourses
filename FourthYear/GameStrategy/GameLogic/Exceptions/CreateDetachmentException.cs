using System;

namespace Strategy.GameLogic.Exceptions
{
    class CreateDetachmentException : Exception
    {
        public CreateDetachmentException(string massage)
            : base(massage)
        {
        }
    }
}
