using System.Collections.Generic;

using TmLox.Interpreter;

namespace TmLox.Functions
{
    public interface IFunction
    {
        AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments);
    }
}
