using System;

namespace TmLox.Interpreter.Errors
{
    public abstract class LoxError : Exception
    {
        protected LoxError()
        {
        }

        protected LoxError(string message)
            : base(message)
        {
        }
    }
}