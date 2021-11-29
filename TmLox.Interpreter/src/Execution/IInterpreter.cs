using System.Collections.Generic;

using TmLox.Interpreter.Ast;
using TmLox.Interpreter.Execution.Functions;

namespace TmLox.Interpreter.Execution
{
    public interface IInterpreter
    {
        void Execute(IList<Statement> statements);

        void Execute(Statement statement);

        AnyValue Evaluate(Statement statement);

        void AddVariable(string name, AnyValue value);

        void AddFunction(ICallable function);

        bool TryGet(string name, out AnyValue value);
    }
}