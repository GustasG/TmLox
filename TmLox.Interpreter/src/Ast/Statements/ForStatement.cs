namespace TmLox.Interpreter.Ast.Statements;

using System.Collections.Generic;

public class ForStatement : Statement
{
    public override NodeType Type => NodeType.For;

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
}