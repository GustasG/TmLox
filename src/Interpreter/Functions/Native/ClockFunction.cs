using System;
using System.Collections.Generic;

namespace TmLox.Interpreter.Functions.Native
{
    class ClockFunction : ICallable
    {
        public AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments)
        {
            return AnyValue.CreateInteger(DateTime.Now.Second);
        }
    }
}
