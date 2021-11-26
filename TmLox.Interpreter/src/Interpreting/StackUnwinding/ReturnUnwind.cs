using System;

namespace TmLox.Interpreter.StackUnwinding
{
    public class ReturnUnwind : Exception
    {
        public AnyValue Value { get; }

        public ReturnUnwind(AnyValue value)
        {
            Value = value;
        }
    }
}
