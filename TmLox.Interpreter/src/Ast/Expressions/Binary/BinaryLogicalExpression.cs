namespace TmLox.Interpreter.Ast.Expressions.Binary
{
    public abstract class BinaryLogicalExpression : BinaryExpression
    {
        public BinaryLogicalExpression(Expression left, Expression right)
            : base(left, right)
        {
        }
    }
}