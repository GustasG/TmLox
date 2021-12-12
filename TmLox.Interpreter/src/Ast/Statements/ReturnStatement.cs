namespace TmLox.Interpreter.Ast.Statements;

public class ReturnStatement : Statement
{
    public override NodeType Type => NodeType.Return;

    public Expression? Value { get; }

    public ReturnStatement(Expression? value)
    {
        Value = value;
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}