namespace TmLox.Interpreter.Execution.StackUnwinding;

using System;

internal class ReturnUnwind : Exception
{
    public AnyValue Value { get; }

    public ReturnUnwind(AnyValue value)
        : base("Return was used outside functions")
    {
        Value = value;
    }
}