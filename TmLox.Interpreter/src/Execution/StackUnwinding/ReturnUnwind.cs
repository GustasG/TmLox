using System;

namespace TmLox.Interpreter.Execution.StackUnwinding
{
    internal class ReturnUnwind : Exception
    {
        public AnyValue Value { get; }

        public ReturnUnwind(AnyValue value)
        {
            Value = value;
        }
    }
}
