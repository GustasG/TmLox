namespace TmLox.Ast.Expressions.Binary.Logical
{
    public class EqualExpression : BinaryLogicalExpression
    {
        public EqualExpression(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override NodeType Type()
        {
            return NodeType.Equal;
        }
    }
}