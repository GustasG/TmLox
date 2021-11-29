using System;

namespace TmLox.Interpreter.Errors
{
    public abstract class LoxError : Exception
    {
        public LoxError()
        {
        }

        public LoxError(string message)
            : base(message)
        {
        }
    }
}