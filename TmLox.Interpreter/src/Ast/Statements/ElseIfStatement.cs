namespace TmLox.Interpreter.Ast.Statements;

using System.Collections.Generic;

public class ElseIfStatement : Statement
{
    public override NodeType Type => NodeType.Elif;

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