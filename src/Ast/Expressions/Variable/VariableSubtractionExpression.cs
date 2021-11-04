namespace TmLox.Ast.Expressions.Variable
{
    public class VariableSubtractionExpression : VariableModificationExpression
    {
        public VariableSubtractionExpression(string variable, Expression value)
            : base(variable, value)
        {
        }

        public VariableSubtractionExpression(VariableExpression variable, Expression value)
            : base(variable, value)
        {
        }
    }
}