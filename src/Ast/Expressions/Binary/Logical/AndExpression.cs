namespace TmLox.Ast.Expressions.Binary.Logical
{
    public class AndExpression : BinaryLogicalExpression
    {
        public AndExpression(Expression left, Expression right)
            : base(left, right)
        {
        }

        public override NodeType Type()
        {
            return NodeType.AndE;
        }
    }
}