using System.Collections.Generic;

namespace TmLox.Interpreter.Functions
{
    public interface ICallable
    {
        bool CheckArity();

        int Arity();

        AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments);        
    }
}
