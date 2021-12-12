namespace TmLox.Interpreter.Execution.Functions;

using System.Collections.Generic;

public interface ICallable
{
    string Name { get; }

    bool CheckArity();

    int Arity();

    AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments);
}