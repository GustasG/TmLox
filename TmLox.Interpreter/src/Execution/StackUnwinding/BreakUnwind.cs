namespace TmLox.Interpreter.Execution.StackUnwinding;

using System;

internal class BreakUnwind : Exception
{
    public BreakUnwind()
        : base("Break was used outside of the loop")
    {
    }
}