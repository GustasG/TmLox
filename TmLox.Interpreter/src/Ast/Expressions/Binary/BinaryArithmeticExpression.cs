namespace TmLox.Interpreter.Ast.Expressions.Binary
{
    public abstract class BinaryArithmeticExpression : BinaryExpression
    {
        public BinaryArithmeticExpression(Expression left, Expression right)
            : base(left, right)
        {
        }
    }
}