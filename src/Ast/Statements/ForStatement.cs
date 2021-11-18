using System.Collections.Generic;

namespace TmLox.Ast.Statements
{
    public class ForStatement : Statement
    {
        public Statement? Initial { get; }

        public Expression? Condition { get; }

        public Expression? Increment { get; }

        public IList<Statement> Body { get; }


        public ForStatement(IList<Statement> body)
        {
            Body = body;
        }

        public ForStatement(Statement? initial, Expression? condition, Expression? increment, IList<Statement> body)
        {
            Initial = initial;
            Condition = condition;
            Increment = increment;
            Body = body;
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.For;
        }
    }
}
