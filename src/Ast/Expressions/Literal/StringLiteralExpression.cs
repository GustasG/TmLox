namespace TmLox.Ast.Expressions.Literal
{
    public class StringLiteralExpression : LiteralExpression<string>
    {
        public StringLiteralExpression(string value)
            : base(value)
        {
        }

        public StringLiteralExpression(Token token)
            : this(token.Value as string)
        {
        }

        public override NodeType Type()
        {
            return NodeType.StringLiteral;
        }
    }
}