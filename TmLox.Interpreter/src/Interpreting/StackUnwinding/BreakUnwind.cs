using System;

namespace TmLox.Interpreter.StackUnwinding
{
    public class BreakUnwind : Exception
    {
        public override string ToString()
        {
            return "Break was used outside of the loop";
        }
    }
}
