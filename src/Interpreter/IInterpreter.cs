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

        void RegisterVariable(string name, AnyValue value);

        void RegisterFunction(string name, IFunction function);

        void AddVariable(string name, AnyValue value);

        void AddFunction(string name, IFunction function);
    }
}