using System.Collections.Generic;

namespace TmLox.Interpreter.Functions
{
    public abstract class NativeFunction : ICallable
    {
        public virtual bool CheckArity()
        {
            return true;
        }

        public abstract int Arity();

        public abstract AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments);
    }
}