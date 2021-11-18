using System.Collections.Generic;

using TmLox.Ast;
using TmLox.Functions;

namespace TmLox.Interpreter
{
    public interface IInterpreter : IVisitor<AnyValue>
    {
        void Execute(IList<Statement> statements);

        void Execute(Statement statement);

        AnyValue Evaluate(Statement statement);

        void RegisterVariable(string name, AnyValue value);

        void RegisterFunction(string name, IFunction function);
    }
}