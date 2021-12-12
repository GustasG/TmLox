namespace TmLox.Interpreter.Ast.Expressions.Unary;

public class UnaryMinusExpression : UnaryExpression
{
    public override NodeType Type => NodeType.UnaryMinus;

    public UnaryMinusExpression(Expression expression)
        : base(expression)
    {
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}