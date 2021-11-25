using System.Collections.Generic;

namespace TmLox.Interpreter.Functions
{
    public interface ICallable
    {
        AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments);
    }
}
