using System.Collections.Generic;

using TmLox.Interpreter.Ast;
using TmLox.Interpreter.Execution.Functions;

namespace TmLox.Interpreter.Execution
{
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
}