namespace TmLox.Ast.Expressions
{
    public abstract class UnaryExpression : Expression
    {
        public Expression Expression { get; }


        public UnaryExpression(Expression expression)
        {
            Expression = expression;
        }
    }
}