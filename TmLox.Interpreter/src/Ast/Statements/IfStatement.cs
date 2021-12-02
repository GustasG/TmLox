using System.Collections.Generic;

namespace TmLox.Interpreter.Ast.Statements
{
    public class ElseIfStatement : Statement
    {
        public override NodeType Type
        {
            get => NodeType.Elif;
        }

        public Expression Condition { get; }

        public IList<Statement> Body { get; }


        public ElseIfStatement(Expression condition, IList<Statement> body)
        {
            Condition = condition;
            Body = body;
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }


    public class IfStatement : Statement
    {
        public override NodeType Type
        {
            get => NodeType.If;
        }

        public Expression Condition { get; }

        public IList<Statement> Body { get; }

        public IList<ElseIfStatement> ElseIfStatements { get; }

        public IList<Statement>? ElseBody { get; }


        public IfStatement(Expression condition, IList<Statement> body, IList<ElseIfStatement> elseIfStatements, IList<Statement>? elseBody)
        {
            Condition = condition;
            Body = body;
            ElseIfStatements = elseIfStatements;
            ElseBody = elseBody;
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}