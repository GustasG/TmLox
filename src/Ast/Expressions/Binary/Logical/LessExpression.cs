namespace TmLox.Ast.Expressions.Binary.Logical
{
    public class LessExpression : BinaryLogicalExpression
    {
        public LessExpression(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override NodeType Type()
        {
            return NodeType.Less;
        }
    }
}