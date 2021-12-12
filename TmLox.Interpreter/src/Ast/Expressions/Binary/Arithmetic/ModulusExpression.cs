namespace TmLox.Interpreter.Ast.Expressions.Binary.Arithmetic;

public class ModulusExpression : BinaryExpression
{
    public override NodeType Type => NodeType.Modulus;

    public ModulusExpression(Expression left, Expression right)
        : base(left, right)
    {
    }

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}