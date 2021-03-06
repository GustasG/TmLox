namespace TmLox.Interpreter.Ast.Expressions.Binary.Arithmetic;

public class MultiplicationExpression : BinaryExpression
{
    public override NodeType Type => NodeType.Multiplication;

    public MultiplicationExpression(Expression left, Expression right)
        : base(left, right)
    {
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}