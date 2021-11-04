namespace TmLox.Ast.Expressions.Binary.Arithmetic
{
    public class DivisionExpression : BinaryArithmeticExpression
    {
        public DivisionExpression(Expression left, Expression right)
            : base(left, right)
        {
        }
    }
}
