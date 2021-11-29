using System.Collections.Generic;

namespace TmLox.Interpreter.Ast.Statements
{
    public class WhileStatement : Statement
    {
        public Expression Condition { get; }

        public IList<Statement> Body { get; }


        public WhileStatement(Expression condition)
        {
            Condition = condition;
            Body = new List<Statement>();
        }

        public WhileStatement(Expression condition, IList<Statement> body)
        {
            Condition = condition;
            Body = body;
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.While;
        }
    }
}
