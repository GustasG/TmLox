namespace TmLox.Ast.Expressions.Binary.Arithmetic
{
    public class ModulusExpression : BinaryArithmeticExpression
    {
        public ModulusExpression(Expression left, Expression right)
            : base(left, right)
        {
        }
    }
}
