using System.Collections.Generic;

namespace TmLox.Interpreter.Execution.Functions
{
    internal abstract class NativeFunction : ICallable
    {
        public string Name { get; }

        public Environment? Environment { set; get; }


        public NativeFunction(string name)
        {
            Name = name;
        }

        public virtual bool CheckArity()
        {
            return true;
        }

        public abstract int Arity();

        public abstract AnyValue Call(IInterpreter interpreter, IList<AnyValue> arguments);

        public override string ToString()
        {
            return $"<built-in function {Name}>";
        }
    }
}