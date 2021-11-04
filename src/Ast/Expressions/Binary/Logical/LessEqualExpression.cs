namespace TmLox.Ast.Expressions.Binary.Logical
{
    public class LessEqualExpression : BinaryLogicalExpression
    {
        public LessEqualExpression(Expression left, Expression right)
            : base(left, right)
        {
        }
    }
}