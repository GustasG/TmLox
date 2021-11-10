namespace TmLox.Ast.Expressions.Unary
{
    public class UnaryNotExpression : UnaryExpression
    {
        public UnaryNotExpression(Expression expression)
            : base(expression)
        {
        }

        public override NodeType Type()
        {
            return NodeType.UnaryNot;
        }
    }
}