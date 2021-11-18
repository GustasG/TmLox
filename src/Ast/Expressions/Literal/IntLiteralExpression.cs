namespace TmLox.Ast.Expressions.Literal
{
    public class IntLiteralExpression : LiteralExpression<long>
    {
        public IntLiteralExpression(long value)
            : base(value)
        {
        }

        public IntLiteralExpression(Token token)
            : this((long)token.Value)
        {
        }

        public override T Accept<T>(IVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override NodeType Type()
        {
            return NodeType.IntLiteral;
        }
    }
}