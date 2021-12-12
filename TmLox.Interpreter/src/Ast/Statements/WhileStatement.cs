namespace TmLox.Interpreter.Ast.Statements;

using System.Collections.Generic;

public class WhileStatement : Statement
{
    public override NodeType Type => NodeType.While;

    public Expression Condition { get; }

    public IList<Statement> Body { get; }

    public WhileStatement(Expression condition, IList<Statement> body)
    {
        Condition = condition;
        Body = body;
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}