namespace TmLox.Interpreter.Errors;

using System;

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