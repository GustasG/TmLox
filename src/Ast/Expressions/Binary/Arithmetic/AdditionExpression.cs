namespace TmLox.Ast.Expressions.Binary.Arithmetic
{
    public class AdditionExpression : BinaryArithmeticExpression
    {
        public AdditionExpression(Expression left, Expression right)
            : base(left, right)
        {
        }
    }
}
