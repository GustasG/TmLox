using System;

namespace TmLox.Errors
{
    public class LoxException : Exception
    {
        public LoxException()
        {
        }

        public LoxException(string message)
            : base(message)
        {
        }
    }
}
