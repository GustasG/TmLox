namespace TmLox.Interpreter.Ast.Expressions.Binary.Logical;

public class LessExpression : BinaryExpression
{
    public override NodeType Type => NodeType.Less;

    public LessExpression(Expression left, Expression right)
        : base(left, right)
    {
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}