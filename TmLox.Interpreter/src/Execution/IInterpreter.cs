namespace TmLox.Interpreter.Execution;

using System.Collections.Generic;

using Ast;
using Functions;

public interface IInterpreter
{
    Environment Environment { get; set; }

    void Execute(IList<Statement> statements);

    void Execute(Statement statement);

    AnyValue Evaluate(Statement statement);

    void Add(string name, AnyValue value);

    void Add(ICallable function);

    bool TryGet(string name, out AnyValue value);
}