namespace TmLox.Interpreter.Ast.Expressions;

public abstract class BinaryExpression : Expression
{
    public Expression Left { get; }

    public Expression Right { get; }

    protected BinaryExpression(Expression left, Expression right)
    {
        Left = left;
        Right = right;
    }
}