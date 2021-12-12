namespace TmLox.Interpreter.Ast.Expressions.Binary.Logical;

public class NotEqualExpression : BinaryExpression
{
    public override NodeType Type => NodeType.NotEqual;

    public NotEqualExpression(Expression left, Expression right)
        : base(left, right)
    {
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}