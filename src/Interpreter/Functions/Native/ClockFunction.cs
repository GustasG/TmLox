using System;
using System.Collections.Generic;

namespace TmLox.Interpreter.Functions.Native
{
    class ClockFunction : IFunction
    {
        public AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments)
        {
            return AnyValue.FromInteger(DateTime.Now.Second);
        }
    }
}
