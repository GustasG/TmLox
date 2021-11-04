namespace TmLox.Ast.Expressions.Variable
{
    public class VariableModulusExpression : VariableModificationExpression
    {
        public VariableModulusExpression(string variable, Expression value)
            : base(variable, value)
        {
        }

        public VariableModulusExpression(VariableExpression variable, Expression value)
            : base(variable, value)
        {
        }
    }
}