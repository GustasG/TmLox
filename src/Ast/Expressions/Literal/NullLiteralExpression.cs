namespace TmLox.Ast.Expressions.Literal
{
    public class NullLiteralExpression : LiteralExpression<object>
    {
        public NullLiteralExpression()
            : base(null)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.NullLiteral;
        }
    }
}