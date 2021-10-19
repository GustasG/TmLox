namespace TmLox.Ast.Expressions
{
    public abstract class LiteralExpression <T> : Expression
    {
        T Value { get; }

        public LiteralExpression(T value)
        {
            Value = value;
        }
    }
}