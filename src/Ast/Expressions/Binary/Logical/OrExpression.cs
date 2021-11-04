namespace TmLox.Ast.Expressions.Binary.Logical
{
    public class OrExpression : BinaryLogicalExpression
    {
        public OrExpression(Expression left, Expression right)
            : base(left, right)
        {
        }
    }
}
