namespace TmLox.Ast.Expressions.Binary.Logical
{
    public class NotEqualExpression : BinaryLogicalExpression
    {
        public NotEqualExpression(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override NodeType Type()
        {
            return NodeType.NotEqual;
        }
    }
}
