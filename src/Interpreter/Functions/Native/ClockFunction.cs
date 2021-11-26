using System;
using System.Collections.Generic;

namespace TmLox.Interpreter.Functions.Native
{
    class ClockFunction : NativeFunction
    {
        public ClockFunction()
            :  base("clock")
        {
        }

        public override int Arity()
        {
            return 0;
        }

        public override AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments)
        {
            return AnyValue.CreateInteger(DateTime.Now.Second);
        }
    }
}
