namespace TmLox.Interpreter.Ast.Expressions.Unary
{
    public class UnaryNotExpression : UnaryExpression
    {
        public override NodeType Type
        {
            get => NodeType.UnaryNot;
        }

        public UnaryNotExpression(Expression expression)
            : base(expression)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}