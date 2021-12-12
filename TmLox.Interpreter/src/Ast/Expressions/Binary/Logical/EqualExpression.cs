namespace TmLox.Interpreter.Ast.Expressions.Binary.Logical;

public class EqualExpression : BinaryExpression
{
    public override NodeType Type => NodeType.Equal;

    public EqualExpression(Expression left, Expression right)
        : base(left, right)
    {
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}