namespace TmLox.Interpreter.Ast.Expressions.Binary
{
    public abstract class BinaryLogicalExpression : BinaryExpression
    {
        protected BinaryLogicalExpression(Expression left, Expression right)
            : base(left, right)
        {
        }
    }
}