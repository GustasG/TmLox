using System;

namespace TmLox.Interpreter.Execution.StackUnwinding
{
    internal class BreakUnwind : Exception
    {
        public BreakUnwind()
            : base("Break was used outside of the loop")
        {

        }
    }
}
