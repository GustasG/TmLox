using System.Collections.Generic;

using TmLox.Ast;
using TmLox.Interpreter.Functions;

namespace TmLox.Interpreter
{
    public interface IInterpreter
    {
        void Execute(IList<Statement> statements);

        void Execute(Statement statement);

        AnyValue Evaluate(Statement statement);

        void AddVariable(string name, AnyValue value);

        void AddFunction(ICallable function);
    }
}