namespace TmLox.Interpreter.Ast.Expressions
{
    public abstract class UnaryExpression : Expression
    {
        public Expression Expression { get; }


        protected UnaryExpression(Expression expression)
        {
            Expression = expression;
        }
    }
}