using System;

namespace TmLox.Interpreter.Execution.StackUnwinding
{
    internal class BreakUnwind : Exception
    {
        public override string ToString()
        {
            return "Break was used outside of the loop";
        }
    }
}
