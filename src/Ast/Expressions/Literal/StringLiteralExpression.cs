namespace TmLox.Ast.Expressions.Literal
{
    public class StringLiteralExpression : LiteralExpression<string>
    {
        public StringLiteralExpression(string value)
            : base(value)
        {
        }

        public StringLiteralExpression(Token token)
            : this((string)token.Value)
        {
        }
    }
}