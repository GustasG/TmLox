namespace TmLox.Interpreter.Ast.Statements;

using System.Collections.Generic;

public class IfStatement : Statement
{
    public override NodeType Type => NodeType.If;

    public Expression Condition { get; }

    public IList<Statement> Body { get; }

    public IList<ElseIfStatement> ElseIfStatements { get; }

    public IList<Statement>? ElseBody { get; }

    public IfStatement(Expression condition, IList<Statement> body, IList<ElseIfStatement> elseIfStatements,
        IList<Statement>? elseBody)
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