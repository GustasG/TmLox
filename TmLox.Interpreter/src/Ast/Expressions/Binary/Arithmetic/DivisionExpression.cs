namespace TmLox.Interpreter.Ast.Expressions.Binary.Arithmetic;

public class DivisionExpression : BinaryExpression
{
    public override NodeType Type => NodeType.Division;

    public DivisionExpression(Expression left, Expression right)
        : base(left, right)
    {
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}