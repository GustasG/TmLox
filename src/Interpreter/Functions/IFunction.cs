using System.Collections.Generic;

namespace TmLox.Interpreter.Functions
{
    public interface IFunction
    {
        AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments);
    }
}
