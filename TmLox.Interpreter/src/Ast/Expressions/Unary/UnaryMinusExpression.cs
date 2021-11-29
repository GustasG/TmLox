namespace TmLox.Interpreter.Ast.Expressions.Unary
{
    public class UnaryMinusExpression : UnaryExpression
    {
        public UnaryMinusExpression(Expression expression)
            : base(expression)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.UnaryMinus;
        }
    }
}