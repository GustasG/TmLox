namespace TmLox.Ast.Expressions.Binary.Arithmetic
{
    public class MultiplicationExpression : BinaryArithmeticExpression
    {
        public MultiplicationExpression(Expression left, Expression right)
            : base(left, right)
        {
        }
    }
}
