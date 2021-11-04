namespace TmLox.Ast.Expressions.Binary.Logical
{
    public class MoreExpression : BinaryLogicalExpression
    {
        public MoreExpression(Expression left, Expression right)
            : base(left, right)
        {
        }
    }
}
