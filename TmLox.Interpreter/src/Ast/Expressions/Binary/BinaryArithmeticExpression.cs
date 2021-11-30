namespace TmLox.Interpreter.Ast.Expressions.Binary
{
    public abstract class BinaryArithmeticExpression : BinaryExpression
    {
        protected BinaryArithmeticExpression(Expression left, Expression right)
            : base(left, right)
        {
        }
    }
}