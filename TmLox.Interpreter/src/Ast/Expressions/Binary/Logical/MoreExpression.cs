namespace TmLox.Interpreter.Ast.Expressions.Binary.Logical;

public class MoreExpression : BinaryExpression
{
    public override NodeType Type => NodeType.More;

    public MoreExpression(Expression left, Expression right)
        : base(left, right)
    {
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}