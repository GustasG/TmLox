namespace TmLox.Interpreter.Execution.Functions.Native;

using System;
using System.Collections.Generic;

internal class ClockFunction : NativeFunction
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