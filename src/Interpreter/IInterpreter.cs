using System.Collections.Generic;

using TmLox.Ast;

namespace TmLox.Interpreter
{
    public interface IInterpreter : IVisitor<AnyValue>
    {
        public void Execute(IList<Statement> statements);

        public AnyValue Execute(Statement statement);

        void AddVariable(string name, AnyValue value);
    }
}
