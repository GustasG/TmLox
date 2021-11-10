namespace TmLox.Ast.Expressions.Binary.Logical
{
    public class LessEqualExpression : BinaryLogicalExpression
    {
        public LessEqualExpression(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override NodeType Type()
        {
            return NodeType.LessEqual;
        }
    }
}