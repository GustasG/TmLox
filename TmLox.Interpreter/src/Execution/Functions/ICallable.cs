using System.Collections.Generic;

namespace TmLox.Interpreter.Execution.Functions
{
    public interface ICallable
    {
        string Name { get; }

        bool CheckArity();

        int Arity();

        AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments);
    }
}