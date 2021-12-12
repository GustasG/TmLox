namespace TmLox.Interpreter.Execution.Functions;

using System.Collections.Generic;

internal abstract class NativeFunction : ICallable
{
    public string Name { get; }

    protected NativeFunction(string name)
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