namespace TmLox.Ast.Expressions.Variable
{
    public class VariableMultiplicationExpression : VariableModificationExpression
    {
        public VariableMultiplicationExpression(string variable, Expression value)
            : base(variable, value)
        {
        }

        public VariableMultiplicationExpression(VariableExpression variable, Expression value)
            : base(variable, value)
        {
        }
    }
}