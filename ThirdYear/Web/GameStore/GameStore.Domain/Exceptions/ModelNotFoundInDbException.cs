using System;

namespace GameStore.Domain.Exceptions
{
    public class ModelNotFoundInDbException : Exception
    {
        public ModelNotFoundInDbException(string info)
            : base(info)
        {
        }
    }
}
