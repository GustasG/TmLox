namespace TmLox.Ast.Expressions.Unary
{
    public class UnaryNotExpression : UnaryExpression
    {
        public UnaryNotExpression(Expression expression)
            : base(expression)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.UnaryNot;
        }
    }
}