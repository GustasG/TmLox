using System.Collections.Generic;

using TmLox.Interpreter;

namespace TmLox
{
    public interface IFunction
    {
        AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments);
    }
}
