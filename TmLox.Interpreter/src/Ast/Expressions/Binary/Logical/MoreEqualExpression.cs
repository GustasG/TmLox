namespace TmLox.Interpreter.Ast.Expressions.Binary.Logical;

public class MoreEqualExpression : BinaryExpression
{
    public override NodeType Type => NodeType.MoreEqual;

    public MoreEqualExpression(Expression left, Expression right)
        : base(left, right)
    {
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}