namespace TmLox.Ast.Expressions.Binary.Logical
{
    public class MoreEqualExpression : BinaryLogicalExpression
    {
        public MoreEqualExpression(Expression left, Expression right)
            : base(left, right)
        {
        }
    }
}
