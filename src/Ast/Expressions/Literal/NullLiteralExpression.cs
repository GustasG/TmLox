namespace TmLox.Ast.Expressions.Literal
{
    public class NullLiteralExpression : LiteralExpression<object>
    {
        public NullLiteralExpression()
            : base(null)
        {
        }
    }
}