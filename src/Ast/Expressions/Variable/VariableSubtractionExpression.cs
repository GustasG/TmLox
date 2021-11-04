namespace TmLox.Ast.Expressions.Variable
{
    public class VariableSubtractionExpression : VariableModificationExpression
    {
        public VariableSubtractionExpression(string variable, Expression value)
            : base(variable, value)
        {
        }

        public VariableSubtractionExpression(Token variable, Expression value)
            : base(variable, value)
        {
        }
    }
}